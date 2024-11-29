import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class CredentialsRepository {
  final FlutterSecureStorage secureStorage;

  CredentialsRepository({this.secureStorage = const FlutterSecureStorage()});

  Future<void> saveAccessToken(String accessToken) async {
    await secureStorage.write(key: 'accessToken', value: accessToken);
  }

  Future<String?> getAccessToken() async {
    return await secureStorage.read(key: 'accessToken');
  }

  Future<void> saveRefreshToken(String refreshToken) async {
    await secureStorage.write(key: 'refreshToken', value: refreshToken);
  }

  Future<String?> getRefreshToken() async {
    return await secureStorage.read(key: 'refreshToken');
  }

  Future<void> deleteTokens() async {
    await secureStorage.delete(key: 'accessToken');
    await secureStorage.delete(key: 'refreshToken');
  }
}
