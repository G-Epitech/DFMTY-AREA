import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/env.dart';
import 'package:triggo/utils/json.dart';

Future<Response<dynamic>> call<T extends Json>({
  required String method,
  required String endpoint,
  T? body,
  String? accessToken,
  http.Client? client,
  Map<String, String>? headers,
}) async {
  final http.Client httpClient = client ?? http.Client();

  try {
    final uri = Uri.parse('${Env.apiUrl}$endpoint');
    print('Calling API: $method $uri');
    final requestHeaders = _buildHeaders(accessToken, headers);

    final response = await _makeRequest(
      httpClient: httpClient,
      method: method,
      uri: uri,
      headers: requestHeaders,
      body: body,
    );

    print('Response: ${response.body.isEmpty ? 'No body' : response.body}');

    final responseJson = _parseResponse(response);

    print('Response JSON: $responseJson');
    if (response.statusCode >= 400) {
      final problemDetails = _parseProblemDetails(responseJson);
      return Response(
        statusCode: Codes.fromStatusCode(response.statusCode),
        message: problemDetails.title ?? '',
        data: null,
        errors: problemDetails.errors,
        headers: response.headers,
      );
    }

    return Response(
      statusCode: Codes.fromStatusCode(response.statusCode),
      message: response.reasonPhrase ?? '',
      data: responseJson,
      errors: null,
      headers: response.headers,
    );
  } catch (e) {
    throw Exception('Failed to call API: $e');
  } finally {
    httpClient.close();
  }
}

Map<String, String> _buildHeaders(
    String? accessToken, Map<String, String>? headers) {
  return {
    'Content-Type': 'application/json; charset=utf-8',
    'Accept': '*/*',
    if (accessToken != null) 'Authorization': 'Bearer $accessToken',
    if (headers != null) ...headers,
  };
}

Future<http.Response> _makeRequest<T extends Json>({
  required http.Client httpClient,
  required String method,
  required Uri uri,
  required Map<String, String> headers,
  T? body,
}) async {
  final encodedBody = body != null ? jsonEncode(body.toJson()) : null;
  final requestMethods = {
    'POST': httpClient.post,
    'PUT': httpClient.put,
    'DELETE': httpClient.delete,
    'GET': (Uri uri, {Map<String, String>? headers, String? body}) =>
        httpClient.get(uri, headers: headers),
  };

  return requestMethods[method]!(uri, headers: headers, body: encodedBody);
}

dynamic _parseResponse(http.Response response) {
  try {
    if (response.body.isEmpty) {
      return null;
    }
    return jsonDecode(response.body);
  } catch (e) {
    throw Exception('Error parsing response: $e');
  }
}

ProblemDetails _parseProblemDetails(Map<String, dynamic> data) {
  try {
    return ProblemDetails(
      type: data['type'] as String?,
      title: data['title'] as String? ?? 'Unknown error',
      status: data['status'] as int? ?? 0,
      detail: data['detail'] as String?,
      instance: data['instance'] as String?,
      errors: (data['errors'] as Map<String, dynamic>?)
          ?.map((key, value) => MapEntry(key, List<String>.from(value))),
    );
  } catch (e) {
    throw Exception('Error parsing problem details: $e');
  }
}
