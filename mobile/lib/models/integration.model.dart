import 'package:flutter/material.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/models/integrations/leagueOfLegends.integration.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
import 'package:triggo/models/integrations/openAI.integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

abstract class Integration {
  final String id;
  final String name;

  Integration({required this.name, required this.id});

  factory Integration.fromDTO(IntegrationDTO dto) {
    switch (dto.type) {
      case IntegrationType.discord:
        return DiscordIntegration.fromDTO(dto);
      case IntegrationType.notion:
        return NotionIntegration.fromDTO(dto);
      case IntegrationType.openAI:
        return OpenAIIntegration.fromDTO(dto);
      case IntegrationType.leagueOfLegends:
        return LeagueOfLegendsIntegration.fromDTO(dto);
      default:
        throw Exception('Unknown integration type');
    }
  }
}

class AvailableIntegration {
  final String identifier;
  final String name;
  final String iconUri;
  final Color color;
  final String url;

  AvailableIntegration({
    required this.identifier,
    required this.name,
    required this.iconUri,
    required this.color,
    required this.url,
  });
}
