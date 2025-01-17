import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/features/integration/integration.names.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/mediator/integrations/discord.mediator.dart';
import 'package:triggo/mediator/integrations/leagueOfLegends.mediator.dart';
import 'package:triggo/mediator/integrations/notion.mediator.dart';
import 'package:triggo/mediator/integrations/openAi.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/automation/automation.repository.dart';
import 'package:triggo/repositories/integration/integration.repository.dart';
import 'package:triggo/utils/launch_url.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;
  final DiscordMediator _discordMediator;
  final NotionMediator _notionMediator;
  final OpenAIMediator _openAIMediator;
  final LeagueOfLegendsMediator _leagueOfLegendsMediator;
  final AutomationRepository _automationRepository;

  IntegrationMediator(this._integrationRepository, this._automationRepository)
      : _discordMediator = DiscordMediator(_integrationRepository.discord),
        _notionMediator = NotionMediator(_integrationRepository.notion),
        _openAIMediator = OpenAIMediator(_integrationRepository.openAI),
        _leagueOfLegendsMediator =
            LeagueOfLegendsMediator(_integrationRepository.leagueOfLegends);

  DiscordMediator get discord => _discordMediator;

  NotionMediator get notion => _notionMediator;

  OpenAIMediator get openAI => _openAIMediator;

  LeagueOfLegendsMediator get leagueOfLegends => _leagueOfLegendsMediator;

  Future<List<Integration>> getUserIntegrations() async {
    List<Integration> integrations = [];
    try {
      final res = await _integrationRepository.getUserIntegrations();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.page.data) {
          integrations.add(Integration.fromDTO(integration));
        }
        return integrations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      throw Exception('Error getting integrations: $e');
    }
  }

  String _getUrlFromName(String name) {
    switch (name) {
      case IntegrationNames.discord:
        return 'discord';
      case IntegrationNames.notion:
        return 'notion';
      case IntegrationNames.openAI:
        return 'openai';
      case IntegrationNames.leagueOfLegends:
        return 'leagueOfLegends';
      default:
        return '';
    }
  }

  Future<List<AvailableIntegration>> getIntegrations() async {
    try {
      final response = await _automationRepository.getAutomationSchema();
      List<AvailableIntegration> integrationNames = [];

      if (response.statusCode == Codes.ok) {
        for (String key in response.data!.schema.keys.toList()) {
          final integration = response.data!.schema[key];
          integrationNames.add(AvailableIntegration(
            name: integration!.name,
            iconUri: integration.iconUri,
            color: HexColor(integration.color),
            url: _getUrlFromName(integration.name),
          ));
        }
      }
      return integrationNames;
    } catch (e) {
      log('Error getting integrations: $e');
      throw Exception('Error getting integrations: $e');
    }
  }

  Future<void> launchURLFromIntegration(String name) async {
    try {
      final res = await _integrationRepository.getIntegrationURI(name);

      if (res.statusCode == Codes.ok && res.data != null) {
        await launchURL(res.data!.uri);
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      rethrow;
    }
  }
}
