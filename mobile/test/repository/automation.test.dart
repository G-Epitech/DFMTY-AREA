import 'dart:convert';

import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mockito/mockito.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/env.dart';
import 'package:triggo/repositories/automation/automation.repository.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';

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
      Uri.parse('${Env.apiUrl}/automations/schema'),
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
                  },
                  "dependencies": {
                    "discord": {
                      "require": "discord-integration",
                      "optional": false
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

    when(mock.get(
      Uri.parse('${Env.apiUrl}/automations/0'),
      headers: {
        'Authorization': 'Bearer dummy',
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': '*/*'
      },
    )).thenAnswer((_) async => http.Response(
          jsonEncode({
            "id": "123",
            "label": "Automation Example",
            "description": "This is an example automation",
            "ownerId": "owner-456",
            "trigger": {
              "identifier": "Discord.MessageReceivedInChannel",
              "parameters": [
                {"identifier": "channel-id", "value": "789"}
              ],
              "dependencies": ["discord-integration"]
            },
            "actions": [
              {
                "identifier": "Discord.SendChannelMessage",
                "parameters": [
                  {
                    "type": "var",
                    "identifier": "message",
                    "value": "Hello, World!"
                  }
                ],
                "dependencies": ["discord-integration"]
              }
            ],
            "enabled": true,
            "updatedAt": "2024-12-10T10:00:00Z"
          }),
          200,
          headers: {'Content-Type': 'application/json'},
        ));

    repository = AutomationRepository(
        client: mock, credentialsRepository: credentialsRepository);
  });

  group('AutomationRepository', () {
    test('getIntegration success', () async {
      final response = await repository.getAutomationSchema();

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data?.schema.length, equals(1));
      expect(response.data?.schema['discord-app']?.name, equals('Discord'));
      expect(response.data?.schema['discord-app']?.iconUri,
          equals('https://example.com/discord.png'));
      expect(response.data?.schema['discord-app']?.color, equals('#7289da'));
      expect(response.data?.schema['discord-app']?.triggers.length, equals(1));
      expect(
          response
              .data?.schema['discord-app']?.triggers['message-received']?.name,
          equals('Message Received'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.description,
          equals('Triggered when a message is received.'));
      expect(
          response
              .data?.schema['discord-app']?.triggers['message-received']?.icon,
          equals('https://example.com/message.png'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.parameters.length,
          equals(1));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.parameters['user']?.name,
          equals('User'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.parameters['user']?.description,
          equals('The user who sent the message.'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.parameters['user']?.type,
          equals('string'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.facts.length,
          equals(1));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.name,
          equals('Message Content'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.description,
          equals('The content of the message.'));
      expect(
          response.data?.schema['discord-app']?.triggers['message-received']
              ?.facts['message-content']?.type,
          equals('string'));
    });

    test('getAutomationById success', () async {
      final response = await repository.getAutomationById('0');

      expect(response.statusCode, equals(Codes.ok));
      expect(response.data!.automation.id, equals('123'));
      expect(response.data!.automation.label, equals('Automation Example'));
      expect(response.data!.automation.description,
          equals('This is an example automation'));
      expect(response.data!.automation.ownerId, equals('owner-456'));
      expect(response.data!.automation.trigger.identifier,
          equals('Discord.MessageReceivedInChannel'));
      expect(response.data!.automation.trigger.parameters.length, equals(1));
      expect(response.data!.automation.trigger.parameters[0].identifier,
          equals('channel-id'));
      expect(
          response.data!.automation.trigger.parameters[0].value, equals('789'));
      expect(response.data!.automation.trigger.dependencies.length, equals(1));
      expect(response.data!.automation.trigger.dependencies[0],
          equals('discord-integration'));
      expect(response.data!.automation.actions.length, equals(1));
      expect(response.data!.automation.actions[0].identifier,
          equals('Discord.SendChannelMessage'));
      expect(response.data!.automation.actions[0].parameters.length, equals(1));
      expect(response.data!.automation.actions[0].parameters[0].type,
          equals('var'));
      expect(response.data!.automation.actions[0].parameters[0].identifier,
          equals('message'));
      expect(response.data!.automation.actions[0].parameters[0].value,
          equals('Hello, World!'));
      expect(
          response.data!.automation.actions[0].dependencies.length, equals(1));
      expect(response.data!.automation.actions[0].dependencies[0],
          equals('discord-integration'));
      expect(response.data!.automation.enabled, equals(true));
      expect(response.data!.automation.updatedAt,
          equals(DateTime.parse('2024-12-10T10:00:00Z')));
    });
  });
}
