import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/models/integration.repository.model.dart';
import 'package:triggo/repositories/models/integrations/discord.integrations.dart';

class DiscordIntegration extends Integration {
  final String username;
  final String email;
  final String displayName;
  final String avatarUri;
  final List<String> flags;

  DiscordIntegration({
    required super.name,
    required this.username,
    required this.email,
    required this.displayName,
    required this.avatarUri,
    required this.flags,
  });

  static DiscordIntegration fromDTO(IntegrationDTO dto) {
    DiscordProperties properties = dto.properties as DiscordProperties;
    return DiscordIntegration(
      name: 'Discord',
      username: properties.username,
      email: properties.email,
      displayName: properties.displayName,
      avatarUri: properties.avatarUri,
      flags: properties.flags,
    );
  }
}
