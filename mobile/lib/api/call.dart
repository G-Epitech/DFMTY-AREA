import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/constants/config/api.dart';

Future<CallResponse<R>?> call<T, R>({
  required String method,
  required String endpoint,
  T? body,
  String? bearerToken,
  http.Client? client,
}) async {
  final http.Client httpClient = client ?? http.Client();

  try {
    final uri = Uri.parse('${Config.apiUrl}$endpoint');
    final headers = _buildHeaders(bearerToken);

    final response = await _makeRequest(
      httpClient: httpClient,
      method: method,
      uri: uri,
      headers: headers,
      body: body,
    );

    final data = _parseResponse<R>(response);
    return CallResponse<R>(
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

  switch (method.toUpperCase()) {
    case 'POST':
      return httpClient.post(uri, headers: headers, body: encodedBody);
    case 'PUT':
      return httpClient.put(uri, headers: headers, body: encodedBody);
    case 'DELETE':
      return httpClient.delete(uri, headers: headers, body: encodedBody);
    case 'GET':
      return httpClient.get(uri, headers: headers);
    default:
      throw Exception('Invalid HTTP method: $method');
  }
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
