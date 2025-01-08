import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/utils/json.dart';

class DiscordPropertiesDTO implements IntegrationPropertiesDTO {
  final String id;
  final String email;
  final String username;
  final String displayName;
  final String avatarUri;
  final List<String> flags;

  DiscordPropertiesDTO({
    required this.id,
    required this.email,
    required this.username,
    required this.displayName,
    required this.avatarUri,
    required this.flags,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'email': email,
      'username': username,
      'displayName': displayName,
      'avatarUri': avatarUri,
      'flags': flags,
    };
  }

  factory DiscordPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return DiscordPropertiesDTO(
      id: json['id'] as String,
      email: json['email'] as String,
      username: json['username'] as String,
      displayName: json['displayName'] as String,
      avatarUri: json['avatarUri'] as String,
      flags: List<String>.from(json['flags'] as List),
    );
  }
}

class DiscordGuildDTO implements Json {
  final String id;
  final String name;
  final String iconUri;
  final int approximateMemberCount;
  final bool linked;

  DiscordGuildDTO({
    required this.id,
    required this.name,
    required this.iconUri,
    required this.approximateMemberCount,
    required this.linked,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'iconUri': iconUri,
      'approximateMemberCount': approximateMemberCount,
      'linked': linked,
    };
  }

  factory DiscordGuildDTO.fromJson(Map<String, dynamic> json) {
    return DiscordGuildDTO(
      id: json['id'] as String,
      name: json['name'] as String,
      iconUri: json['iconUri'] as String,
      approximateMemberCount: json['approximateMemberCount'] as int,
      linked: json['linked'] as bool,
    );
  }
}

class DiscordChannelDTO implements Json {
  final String id;
  final String name;

  DiscordChannelDTO({
    required this.id,
    required this.name,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
    };
  }

  factory DiscordChannelDTO.fromJson(Map<String, dynamic> json) {
    return DiscordChannelDTO(
      id: json['id'] as String,
      name: json['name'] as String,
    );
  }
}
