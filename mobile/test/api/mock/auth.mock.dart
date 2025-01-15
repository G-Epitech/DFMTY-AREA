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
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 400,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
        }),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 400,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
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
          'accessToken': 'dummy-access-token',
          'refreshToken': 'dummy-refresh-token'
        }),
        200,
      );
    } else {
      return http.Response(
        jsonEncode({
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 401,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
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
  )).thenAnswer((invocation) async {
    if (invocation.namedArguments[const Symbol('headers')] == null) {
      return http.Response(
        jsonEncode({
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 400,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
        }),
        400,
      );
    }

    final headers = invocation.namedArguments[const Symbol('headers')]
        as Map<String, String>;
    final lastToken = headers['Authorization']!.split(' ')[1];

    if (lastToken == 'dummy-refresh-token') {
      return http.Response(
        jsonEncode({
          'accessToken': 'dummy-access-token',
          'refreshToken': 'dummy-refresh-token',
        }),
        200,
      );
    } else {
      return http.Response(
        jsonEncode({
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 401,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
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
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 400,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
        }),
        400,
      );
    }
    if (invocation.namedArguments[const Symbol('body')] == null) {
      return http.Response(
        jsonEncode({
          "type":
              "https://https://datatracker.ietf.org/doc/html/rfc7231#section-**",
          "title": "Short human-readable title",
          "status": 400,
          "detail": "Human-readable explanation",
          "instance": "/**/endpoint-path"
        }),
        400,
      );
    }

    return http.Response(
      jsonEncode({
        'accessToken': 'dummy-access-token',
        'refreshToken': 'dummy-refresh-token',
      }),
      201,
    );
  });
}
