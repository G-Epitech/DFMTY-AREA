import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/env.dart';

import '../call.test.mocks.dart';

void authMock(MockClient mock) {
  _loginMock(mock);
  _refreshTokenMock(mock);
  _registerMock(mock);
}

void _loginMock(MockClient mock) {
  when(mock.post(
    Uri.parse('${Env.apiUrl}/auth/login'),
    headers: anyNamed('headers'),
    body: anyNamed('body'),
  )).thenAnswer((invocation) async {
    if (invocation.namedArguments[const Symbol('headers')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No headers']
        }),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No body']
        }),
        400,
      );
    }

    final body = invocation.namedArguments[const Symbol('body')] as String;

    final Map<String, dynamic> data = jsonDecode(body);

    if (data['email'] == 'john.doe@example.com' &&
        data['password'] == 'password123') {
      return http.Response(
        jsonEncode({
          'data': {
            'token': 'dummy-token',
            'refreshToken': 'dummy-refresh-token'
          }
        }),
        200,
      );
    } else {
      return http.Response(
        jsonEncode({
          'errors': ['Invalid credentials']
        }),
        401,
      );
    }
  });
}

void _refreshTokenMock(MockClient mock) {
  when(mock.post(
    Uri.parse('${Env.apiUrl}/auth/refresh'),
    headers: anyNamed('headers'),
    body: anyNamed('body'),
  )).thenAnswer((invocation) async {
    if (invocation.namedArguments[const Symbol('headers')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No headers']
        }),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No body']
        }),
        400,
      );
    }

    final body = invocation.namedArguments[const Symbol('body')] as String;

    final Map<String, dynamic> data = jsonDecode(body);

    if (data['refreshToken'] == 'dummy-refresh-token') {
      return http.Response(
        jsonEncode({
          'data': {
            'token': 'dummy-token',
            'refreshToken': 'dummy-refresh-token'
          }
        }),
        200,
      );
    } else {
      return http.Response(
        jsonEncode({
          'errors': ['Invalid refresh token']
        }),
        401,
      );
    }
  });
}

void _registerMock(MockClient mock) {
  when(mock.post(
    Uri.parse('${Env.apiUrl}/auth/register'),
    headers: anyNamed('headers'),
    body: anyNamed('body'),
  )).thenAnswer((invocation) async {
    if (invocation.namedArguments[const Symbol('headers')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No headers']
        }),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({
          'errors': ['No body']
        }),
        400,
      );
    }

    return http.Response(
      jsonEncode({
        'data': {
          'token': 'dummy-token',
          'refreshToken': 'dummy-refresh-token',
        }
      }),
      201,
    );
  });
}
