import 'package:flutter/material.dart';
import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/models/integrations/notion.integration.model.dart';
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
      default:
        throw Exception('Unknown integration type');
    }
  }
}

class AvailableIntegration {
  final String name;
  final String iconUri;
  final Color color;
  final String url;

  AvailableIntegration({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.url,
  });
}
