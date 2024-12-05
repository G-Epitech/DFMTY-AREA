import 'package:triggo/utils/json.dart';

import 'integrations/discord.integrations.dart';

class IntegrationType {
  static const String discord = 'Discord';
  static const String gmail = 'Gmail';
}

abstract class IntegrationProperties implements Json {
  @override
  Map<String, dynamic> toJson();
}

class Integration implements Json {
  late final String id;
  late final String ownerId;
  late final String type;
  late final bool isValid;
  late final IntegrationProperties properties;

  Integration({
    required this.id,
    required this.ownerId,
    required this.type,
    required this.isValid,
    required this.properties,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'ownerId': ownerId,
      'type': type,
      'isValid': isValid,
      'properties': properties.toJson(),
    };
  }

  factory Integration.fromJson(Map<String, dynamic> json) {
    IntegrationProperties properties;
    switch (json['type']) {
      case IntegrationType.discord:
        properties = DiscordProperties.fromJson(json['properties']);
        break;
      default:
        throw Exception('Unknown integration type');
    }

    return Integration(
      id: json['id'] as String,
      ownerId: json['ownerId'] as String,
      type: json['type'] as String,
      isValid: json['isValid'] as bool,
      properties: properties,
    );
  }
}
