import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/annotations.dart';
import 'package:triggo/api/call.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/dtos/authentication.dtos.dart';

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
      final response = await call<InLoginDTO>(
        method: 'POST',
        endpoint: '/auth/login',
        body:
            InLoginDTO(email: 'john.doe@example.com', password: 'password123'),
        bearerToken: null,
        client: mockClient,
      );

      expect(response, isNotNull);
      expect(response.statusCode, equals(Codes.ok));
      expect(response.data['token'], equals('dummy-token'));
    });

    test('Failed login request with invalid credentials', () async {
      final response = await call<InLoginDTO>(
        method: 'POST',
        endpoint: '/auth/login',
        body: InLoginDTO(
            email: 'john.doe@example.com', password: 'wrong-password'),
        bearerToken: null,
        client: mockClient,
      );

      expect(response, isNotNull);
      expect(response.statusCode, equals(Codes.unauthorized));
      expect(response.errors, equals(['Invalid credentials']));
    });
  });
}
