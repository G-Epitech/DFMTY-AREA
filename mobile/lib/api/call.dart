import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/env.dart';

Future<Response<R>> call<T, R>({
  required String method,
  required String endpoint,
  T? body,
  String? bearerToken,
  http.Client? client,
}) async {
  final http.Client httpClient = client ?? http.Client();

  try {
    final uri = Uri.parse('${Env.apiUrl}$endpoint');
    final headers = _buildHeaders(bearerToken);

    final response = await _makeRequest(
      httpClient: httpClient,
      method: method,
      uri: uri,
      headers: headers,
      body: body,
    );

    final data = _parseResponse<R>(response);
    return Response<R>(
      statusCode: Codes.fromStatusCode(response.statusCode),
      message: response.reasonPhrase ?? '',
      data: data,
    );
  } catch (e) {
    throw Exception('Error making request: $e');
  } finally {
    httpClient.close();
  }
}

Map<String, String> _buildHeaders(String? bearerToken) {
  return {
    'Content-Type': 'application/json',
    if (bearerToken != null) 'Authorization': 'Bearer $bearerToken',
  };
}

Future<http.Response> _makeRequest<T>({
  required http.Client httpClient,
  required String method,
  required Uri uri,
  required Map<String, String> headers,
  T? body,
}) async {
  final encodedBody = body != null ? jsonEncode(body) : null;
  final requestMethods = {
    'POST': httpClient.post,
    'PUT': httpClient.put,
    'DELETE': httpClient.delete,
    'GET': (Uri uri, {Map<String, String>? headers, String? body}) =>
        httpClient.get(uri, headers: headers),
  };

  return requestMethods[method]!(uri, headers: headers, body: encodedBody);
}

R? _parseResponse<R>(http.Response response) {
  try {
    final json = jsonDecode(response.body);
    if (json is R) {
      return json;
    } else {
      throw Exception('Invalid response type');
    }
  } catch (e) {
    throw Exception('Error parsing response: $e');
  }
}
