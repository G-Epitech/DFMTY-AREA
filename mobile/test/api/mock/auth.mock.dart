import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/env.dart';

import '../call.test.mocks.dart';

void authMock(MockClient mock) {
  when(mock.post(
    Uri.parse('${Env.apiUrl}/auth/login'),
    headers: anyNamed('headers'),
    body: anyNamed('body'),
  )).thenAnswer((invocation) async {
    if (invocation.namedArguments[const Symbol('headers')] == null) {
      return http.Response(
        jsonEncode({'error': 'No headers'}),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({'error': 'No body'}),
        400,
      );
    }

    final body = invocation.namedArguments[const Symbol('body')] as String;

    final Map<String, dynamic> data = jsonDecode(body);

    if (data['username'] == 'emilys' && data['password'] == 'emilyspass') {
      return http.Response(
        jsonEncode({'token': 'dummy-token'}),
        200,
      );
    } else {
      return http.Response(
        jsonEncode({'error': 'Invalid credentials'}),
        401,
      );
    }
  });
}
