import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/discord.integrations.dart';

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
    DiscordPropertiesDTO properties = dto.properties as DiscordPropertiesDTO;
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

class DiscordGuild {
  final String id;
  final String name;
  final String iconUri;
  final int approximateMemberCount;
  final bool linked;

  DiscordGuild({
    required this.id,
    required this.name,
    required this.iconUri,
    required this.approximateMemberCount,
    required this.linked,
  });

  static DiscordGuild fromDTO(DiscordGuildDTO dto) {
    return DiscordGuild(
      id: dto.id,
      name: dto.name,
      iconUri: dto.iconUri,
      approximateMemberCount: dto.approximateMemberCount,
      linked: dto.linked,
    );
  }
}
