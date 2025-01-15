import 'package:triggo/repositories/integration/models/integrations/discord.integrations.dart';
import 'package:triggo/repositories/integration/models/integrations/notion.integrations.dart';
import 'package:triggo/repositories/integration/models/integrations/openAI.integrations.dart';
import 'package:triggo/utils/json.dart';

class IntegrationType {
  static const String discord = 'Discord';
  static const String notion = 'Notion';
  static const String openAI = 'OpenAi';
}

abstract class IntegrationPropertiesDTO implements Json {
  @override
  Map<String, dynamic> toJson();
}

class IntegrationDTO implements Json {
  late final String id;
  late final String ownerId;
  late final String type;
  late final bool isValid;
  late final IntegrationPropertiesDTO properties;

  IntegrationDTO({
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

  factory IntegrationDTO.fromJson(Map<String, dynamic> json) {
    IntegrationPropertiesDTO properties;
    switch (json['type']) {
      case IntegrationType.discord:
        properties = DiscordPropertiesDTO.fromJson(json['properties']);
        break;
      case IntegrationType.notion:
        properties = NotionPropertiesDTO.fromJson(json['properties']);
      case IntegrationType.openAI:
        properties = OpenAIPropertiesDTO.fromJson(json['properties']);
      default:
        throw Exception('Unknown integration type');
    }

    return IntegrationDTO(
      id: json['id'] as String,
      ownerId: json['ownerId'] as String,
      type: json['type'] as String,
      isValid: json['isValid'] as bool,
      properties: properties,
    );
  }
}
