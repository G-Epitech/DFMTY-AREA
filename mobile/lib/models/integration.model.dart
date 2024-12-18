import 'package:triggo/models/integrations/discord.integration.model.dart';
import 'package:triggo/repositories/models/integration.repository.model.dart';

abstract class Integration {
  final String name;

  Integration({required this.name});

  factory Integration.fromDTO(IntegrationDTO dto) {
    switch (dto.type) {
      case IntegrationType.discord:
        return DiscordIntegration.fromDTO(dto);
      default:
        throw Exception('Unknown integration type');
    }
  }
}
