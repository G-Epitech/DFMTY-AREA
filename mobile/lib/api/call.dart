import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:triggo/api/response.dart';
import 'package:triggo/constants/config/api.dart';

Future<CallResponse<R>?> call<T, R>({
  required String method,
  required String endpoint,
  T? body,
  String? bearerToken,
}) async {
  if (Config.apiUrl.isEmpty) {
    throw Exception('API_URL is not defined in config file');
  }

  try {
    final uri = Uri.parse('${Config.apiUrl}$endpoint');
    final headers = {
      'Content-Type': 'application/json',
      if (bearerToken != null) 'Authorization': 'Bearer $bearerToken',
    };

    http.Response response;
    switch (method.toUpperCase()) {
      case 'POST':
        response = await http.post(uri,
            headers: headers, body: body != null ? jsonEncode(body) : null);
        break;
      case 'PUT':
        response = await http.put(uri,
            headers: headers, body: body != null ? jsonEncode(body) : null);
        break;
      case 'DELETE':
        response = await http.delete(uri,
            headers: headers, body: body != null ? jsonEncode(body) : null);
        break;
      case 'GET':
      default:
        response = await http.get(uri, headers: headers);
    }

    final json = jsonDecode(response.body);

    return CallResponse<R>(
      statusCode: response.statusCode,
      message: response.reasonPhrase ?? '',
      data: json as R?,
    );
  } catch (e) {
    print('Error: $e');
    return null;
  }
}
