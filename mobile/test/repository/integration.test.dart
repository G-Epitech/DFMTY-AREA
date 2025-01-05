import 'dart:convert';

import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/env.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/integration.repository.dart';
import 'package:triggo/repositories/models/integrations/discord.integrations.dart';

import '../api/call.test.mocks.dart';
import '../api/mock/init.mock.dart';
import 'credentials.test.mocks.dart';

void integrationRepositoryTests() {
  late MockClient mock;
  late IntegrationRepository repository;
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
      Uri.parse('${Env.apiUrl}/user/integrations'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 2, "pageSize": 10, "totalPages": 1, "totalRecords": 1,'
          '"data": [{"id": "0", "ownerId": "0", "type": "Discord", "isValid": true, "properties": {'
          '"id": "0", "email": "example@example.com", "username": "example", "displayName": "example", "avatarUri": "example", "flags": []}}]'
          '}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    when(mock.get(
      Uri.parse('${Env.apiUrl}/user/integrations/?page=1'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 1, "pageSize": 10, "totalPages": 1, "totalRecords": 1,'
          '"data": []'
          '}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    when(mock.get(
      Uri.parse('${Env.apiUrl}/user/integrations/?page=0&size=10'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          '{"pageNumber": 2, "pageSize": 10, "totalPages": 1, "totalRecords": 1,'
          '"data": [{"id": "0", "ownerId": "0", "type": "Discord", "isValid": true, "properties": {'
          '"id": "0", "email": "example@example.com", "username": "example", "displayName": "example", "avatarUri": "example", "flags": []}}]'
          '}',
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    when(mock.get(
      Uri.parse('${Env.apiUrl}/user/integrations/0'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          jsonEncode({
            "id": "0",
            "ownerId": "0",
            "type": "Discord",
            "isValid": true,
            "properties": {
              "id": "0",
              "email": "example@example.com",
              "username": "example",
              "displayName": "example",
              "avatarUri": "example",
              "flags": ["Test"]
            }
          }),
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    repository = IntegrationRepository(
        client: mock, credentialsRepository: credentialsRepository);
  });

  group('IntegrationRepository', () {
    test('getIntegration success', () async {
      final response = await repository.getUserIntegrations();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.page.pageNumber, 2);
      expect(response.data?.page.pageSize, 10);
      expect(response.data?.page.totalPages, 1);
      expect(response.data?.page.totalRecords, 1);
      expect(response.data?.page.data[0].toJson(), {
        'id': '0',
        'ownerId': '0',
        'type': 'Discord',
        'isValid': true,
        'properties': DiscordProperties(
          id: '0',
          email: 'example@example.com',
          username: 'example',
          displayName: 'example',
          avatarUri: 'example',
          flags: [],
        ).toJson()
      });
    });
    test('getIntegrations success', () async {
      final response = await repository.getUserIntegrations();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.page.pageNumber, 2);
      expect(response.data?.page.pageSize, 10);
      expect(response.data?.page.totalPages, 1);
      expect(response.data?.page.totalRecords, 1);
      expect(response.data?.page.data[0].toJson(), {
        'id': '0',
        'ownerId': '0',
        'type': 'Discord',
        'isValid': true,
        'properties': DiscordProperties(
          id: '0',
          email: 'example@example.com',
          username: 'example',
          displayName: 'example',
          avatarUri: 'example',
          flags: [],
        ).toJson()
      });
    });

    test('getIntegrations success', () async {
      final response = await repository.getUserIntegrations(page: 0, size: 10);

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.page.pageNumber, 2);
      expect(response.data?.page.pageSize, 10);
      expect(response.data?.page.totalPages, 1);
      expect(response.data?.page.totalRecords, 1);
      expect(response.data?.page.data[0].toJson(), {
        'id': '0',
        'ownerId': '0',
        'type': 'Discord',
        'isValid': true,
        'properties': DiscordProperties(
          id: '0',
          email: 'example@example.com',
          username: 'example',
          displayName: 'example',
          avatarUri: 'example',
          flags: [],
        ).toJson()
      });
    });

    test('getIntegrationById success', () async {
      final response = await repository.getUserIntegrationById('0');

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data!.integration.toJson(), {
        'id': '0',
        'ownerId': '0',
        'type': 'Discord',
        'isValid': true,
        'properties': DiscordProperties(
          id: '0',
          email: 'example@example.com',
          username: 'example',
          displayName: 'example',
          avatarUri: 'example',
          flags: ['Test'],
        ).toJson()
      });
    });
  });
}
