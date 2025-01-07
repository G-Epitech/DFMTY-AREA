import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class DiscordProperties implements IntegrationPropertiesDTO {
  final String id;
  final String email;
  final String username;
  final String displayName;
  final String avatarUri;
  final List<String> flags;

  DiscordProperties({
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

  factory DiscordProperties.fromJson(Map<String, dynamic> json) {
    return DiscordProperties(
      id: json['id'] as String,
      email: json['email'] as String,
      username: json['username'] as String,
      displayName: json['displayName'] as String,
      avatarUri: json['avatarUri'] as String,
      flags: List<String>.from(json['flags'] as List),
    );
  }
}
