import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/annotations.dart';
import 'package:triggo/api/call.dart';
import 'package:triggo/api/codes.dart';

import 'call.test.mocks.dart';
import 'mock/init.mock.dart';

@GenerateMocks([http.Client])
void apiTests() {
  group('API Caller Tests', () {
    late MockClient mockClient;

    setUp(() {
      mockClient = MockClient();
      initMock(mockClient);
    });

    test('Successful login request', () async {
      final response = await call<Map<String, dynamic>, dynamic>(
        method: 'POST',
        endpoint: '/auth/login',
        body: {'username': 'emilys', 'password': 'emilyspass'},
        bearerToken: null,
        client: mockClient,
      );

      expect(response, isNotNull);
      expect(response!.statusCode, equals(Codes.ok));
      expect(response.data['token'], equals('dummy-token'));
    });

    test('Failed login request with invalid credentials', () async {
      final response = await call<Map<String, dynamic>, dynamic>(
        method: 'POST',
        endpoint: '/auth/login',
        body: {'username': 'wronguser', 'password': 'wrongpass'},
        bearerToken: null,
        client: mockClient,
      );

      expect(response, isNotNull);
      expect(response!.statusCode, equals(Codes.unauthorized));
      expect(response.data['error'], equals('Invalid credentials'));
    });
  });
}
