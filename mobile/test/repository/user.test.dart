import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/env.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/user.repository.dart';

import '../api/call.test.mocks.dart';
import '../api/mock/init.mock.dart';
import 'credentials.test.mocks.dart';

void userRepositoryTests() {
  late MockClient mock;
  late UserRepository repository;
  late CredentialsRepository credentialsRepository;
  late MockFlutterSecureStorage mockSecureStorage;

  setUp(() {
    mock = MockClient();
    initMock(mock);

    mockSecureStorage = MockFlutterSecureStorage();
    when(mockSecureStorage.read(key: 'accessToken'))
        .thenAnswer((_) async => 'dummy');

    credentialsRepository =
        CredentialsRepository(secureStorage: mockSecureStorage);

    when(mock.get(
      Uri.parse('${Env.apiUrl}/user'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          '{'
          '"id": "example",'
          '"firstName": "example",'
          '"lastName": "example",'
          '"email": "example@example.com",'
          '"picture": "example"'
          '}',
          200,
        ));

    repository = UserRepository(
        client: mock, credentialsRepository: credentialsRepository);
  });

  group('UserRepository', () {
    test('getUser success', () async {
      final response = await repository.getUser();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.id, "example");
      expect(response.data?.firstName, "example");
      expect(response.data?.lastName, "example");
      expect(response.data?.email, "example@example.com");
      expect(response.data?.picture, "example");
    });
  });
}
