import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:mockito/annotations.dart';
import 'package:mockito/mockito.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';

import 'credentials.test.mocks.dart';

@GenerateMocks([FlutterSecureStorage])
void credentialsRepositoryTests() {
  late CredentialsRepository repository;
  late MockFlutterSecureStorage mockSecureStorage;

  setUp(() {
    mockSecureStorage = MockFlutterSecureStorage();
    repository = CredentialsRepository(secureStorage: mockSecureStorage);
  });

  group('CredentialsRepository', () {
    test('saveAccessToken writes accessToken to secure storage', () async {
      const accessToken = 'dummyAccessToken';

      await repository.saveAccessToken(accessToken);

      verify(mockSecureStorage.write(key: 'accessToken', value: accessToken))
          .called(1);
    });

    test('getAccessToken reads accessToken from secure storage', () async {
      const accessToken = 'dummyAccessToken';
      when(mockSecureStorage.read(key: 'accessToken'))
          .thenAnswer((_) async => accessToken);

      final result = await repository.getAccessToken();

      expect(result, accessToken);
      verify(mockSecureStorage.read(key: 'accessToken')).called(1);
    });

    test('saveRefreshToken writes refreshToken to secure storage', () async {
      const refreshToken = 'dummyRefreshToken';

      await repository.saveRefreshToken(refreshToken);

      verify(mockSecureStorage.write(key: 'refreshToken', value: refreshToken))
          .called(1);
    });

    test('getRefreshToken reads refreshToken from secure storage', () async {
      const refreshToken = 'dummyRefreshToken';
      when(mockSecureStorage.read(key: 'refreshToken'))
          .thenAnswer((_) async => refreshToken);

      final result = await repository.getRefreshToken();

      expect(result, refreshToken);
      verify(mockSecureStorage.read(key: 'refreshToken')).called(1);
    });

    test(
        'deleteTokens removes accessToken and refreshToken from secure storage',
        () async {
      await repository.deleteTokens();

      verify(mockSecureStorage.delete(key: 'accessToken')).called(1);
      verify(mockSecureStorage.delete(key: 'refreshToken')).called(1);
    });
  });
}
