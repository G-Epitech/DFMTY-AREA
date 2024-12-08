import 'package:flutter_test/flutter_test.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/repositories/authentication.repository.dart';

import '../api/call.test.mocks.dart';
import '../api/mock/init.mock.dart';

void authRepositoryTests() {
  late MockClient mock;
  late AuthenticationRepository repository;

  setUp(() {
    mock = MockClient();
    initMock(mock);
    repository = AuthenticationRepository(client: mock);
  });

  group('AuthenticationRepository', () {
    test('login success', () async {
      const email = 'john.doe@example.com';
      const password = 'password123';

      final response = await repository.login(email, password);

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.accessToken, 'dummy-access-token');
      expect(response.data?.refreshToken, 'dummy-refresh-token');
    });

    test('login failure', () async {
      const email = 'john.doe@example.com';
      const password = 'wrong-password';

      final response = await repository.login(email, password);

      expect(response.statusCode, equals(Codes.unauthorized));
    });

    test('refreshToken success', () async {
      const refreshToken = 'dummy-refresh-token';

      final response = await repository.refreshToken(refreshToken);

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.accessToken, 'dummy-access-token');
    });

    test('register success', () async {
      const email = 'newuser@example.com';
      const password = 'password123';
      const firstName = 'New';
      const lastName = 'User';

      final response =
          await repository.register(email, password, firstName, lastName);

      expect(response.statusCode, equals(Codes.created));
      expect(response.data, isNotNull);
      expect(response.data?.accessToken, isNotNull);
      expect(response.data?.refreshToken, isNotNull);
    });
  });
}
