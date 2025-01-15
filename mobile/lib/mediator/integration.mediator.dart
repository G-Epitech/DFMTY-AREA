import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/mediator/integrations/discord.mediator.dart';
import 'package:triggo/mediator/integrations/leagueOfLegends.mediator.dart';
import 'package:triggo/mediator/integrations/notion.mediator.dart';
import 'package:triggo/mediator/integrations/openAi.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/integration.repository.dart';
import 'package:triggo/utils/launch_url.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;
  final DiscordMediator _discordMediator;
  final NotionMediator _notionMediator;
  final OpenAIMediator _openAIMediator;
  final LeagueOfLegendsMediator _leagueOfLegendsMediator;

  IntegrationMediator(this._integrationRepository)
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
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<List<AvailableIntegration>> getIntegrations() async {
    List<AvailableIntegration> integrations = [];
    try {
      final res = await _integrationRepository.getIntegrationNames();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.page.data) {
          integrations.add(AvailableIntegration(
            name: integration.name,
            iconUri: integration.iconUri,
            color: HexColor(integration.color),
            url: integration.url,
          ));
        }
        return integrations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      log('Error getting integrations: $e');
      // Display error message with a snackbar or dialog (something like that)
      return [];
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
      // Display error message with a snackbar or dialog (something like that)
      rethrow;
    }
  }
}
