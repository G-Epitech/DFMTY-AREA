import 'dart:convert';

import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/env.dart';
import 'package:triggo/repositories/automation.repository.dart';
import 'package:triggo/repositories/credentials.repository.dart';

import '../api/call.test.mocks.dart';
import '../api/mock/init.mock.dart';
import 'credentials.test.mocks.dart';

void automationRepositoryTests() {
  late MockClient mock;
  late AutomationRepository repository;
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
      Uri.parse('${Env.apiUrl}/automations/manifest'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          jsonEncode({
            "discord-app": {
              "name": "Discord",
              "iconUri": "https://example.com/discord.png",
              "color": "#7289da",
              "triggers": {
                "message-received": {
                  "name": "Message Received",
                  "description": "Triggered when a message is received.",
                  "icon": "https://example.com/message.png",
                  "parameters": {
                    "user": {
                      "name": "User",
                      "description": "The user who sent the message.",
                      "type": "string"
                    }
                  },
                  "facts": {
                    "message-content": {
                      "name": "Message Content",
                      "description": "The content of the message.",
                      "type": "string"
                    }
                  }
                }
              },
              "actions": {}
            }
          }),
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    repository = AutomationRepository(
        client: mock, credentialsRepository: credentialsRepository);
  });

  group('AutomationRepository', () {
    test('getIntegration success', () async {
      final response = await repository.getAutomationManifest();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.manifests.length, equals(1));
      expect(response.data?.manifests['discord-app']?.name, equals('Discord'));
      expect(response.data?.manifests['discord-app']?.iconUri,
          equals('https://example.com/discord.png'));
      expect(response.data?.manifests['discord-app']?.color, equals('#7289da'));
      expect(
          response.data?.manifests['discord-app']?.triggers.length, equals(1));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.name,
          equals('Message Received'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.description,
          equals('Triggered when a message is received.'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.icon,
          equals('https://example.com/message.png'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.parameters.length,
          equals(1));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.parameters['user']?.name,
          equals('User'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.parameters['user']?.description,
          equals('The user who sent the message.'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.parameters['user']?.type,
          equals('string'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.facts.length,
          equals(1));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.name,
          equals('Message Content'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.description,
          equals('The content of the message.'));
      expect(
          response.data?.manifests['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.type,
          equals('string'));
    });
  });
}
