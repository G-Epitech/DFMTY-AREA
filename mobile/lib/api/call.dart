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
    final headers = {
      'Content-Type': 'application/json',
      if (bearerToken != null) 'Authorization': 'Bearer $bearerToken',
    };

    http.Response response;
    switch (method.toUpperCase()) {
      case 'POST':
        response = await httpClient.post(
          uri,
          headers: headers,
          body: body != null ? jsonEncode(body) : null,
        );
        break;
      case 'PUT':
        response = await httpClient.put(
          uri,
          headers: headers,
          body: body != null ? jsonEncode(body) : null,
        );
        break;
      case 'DELETE':
        response = await httpClient.delete(
          uri,
          headers: headers,
          body: body != null ? jsonEncode(body) : null,
        );
        break;
      case 'GET':
        response = await httpClient.get(uri, headers: headers);
        break;
      default:
        throw Exception('Invalid method');
    }

    final json = jsonDecode(response.body);

    return CallResponse<R>(
      statusCode: Codes.fromStatusCode(response.statusCode),
      message: response.reasonPhrase ?? '',
      data: json as R?,
    );
  } catch (e) {
    print('Error in Call Function: $e');
    return null;
  }
}
