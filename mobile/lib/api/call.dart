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
  String? bearerToken,
  http.Client? client,
  Map<String, String>? headers,
}) async {
  final http.Client httpClient = client ?? http.Client();

  try {
    final uri = Uri.parse('${Env.apiUrl}$endpoint');
    final requestHeaders = _buildHeaders(bearerToken, headers);

    final response = await _makeRequest(
      httpClient: httpClient,
      method: method,
      uri: uri,
      headers: requestHeaders,
      body: body,
    );

    final responseJson = _parseResponse(response);
    return Response(
      statusCode: Codes.fromStatusCode(response.statusCode),
      message: response.reasonPhrase ?? '',
      data: responseJson,
      errors: _parseErrors(responseJson),
      headers: response.headers,
    );
  } catch (e) {
    rethrow;
  } finally {
    httpClient.close();
  }
}

Map<String, String> _buildHeaders(
    String? bearerToken, Map<String, String>? headers) {
  return {
    'Content-Type': 'application/json',
    if (bearerToken != null) 'Authorization': 'Bearer $bearerToken',
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
    return jsonDecode(response.body);
  } catch (e) {
    throw Exception('Error parsing response: $e');
  }
}

List<String>? _parseErrors(Map<String, dynamic> data) {
  if (data.containsKey('errors')) {
    return List<String>.from(data['errors']);
  }

  return null;
}
