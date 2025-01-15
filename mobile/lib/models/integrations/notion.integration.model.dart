import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/notion.integrations.dart';

class NotionIntegration extends Integration {
  final String username;
  final String email;
  final String avatarUri;
  final String workspaceName;

  NotionIntegration({
    required super.name,
    required super.id,
    required this.username,
    required this.email,
    required this.avatarUri,
    required this.workspaceName,
  });

  static NotionIntegration fromDTO(IntegrationDTO dto) {
    NotionPropertiesDTO properties = dto.properties as NotionPropertiesDTO;
    return NotionIntegration(
      name: 'Notion',
      id: dto.id,
      username: properties.name,
      email: properties.email,
      avatarUri: properties.avatarUri,
      workspaceName: properties.workspaceName,
    );
  }
}

class NotionDatabase {
  final String id;
  final String title;
  final String? description;
  final String? iconUri;
  final String uri;

  NotionDatabase({
    required this.id,
    required this.title,
    this.description,
    this.iconUri,
    required this.uri,
  });

  static NotionDatabase fromDTO(NotionDatabaseDTO dto) {
    return NotionDatabase(
      id: dto.id,
      title: dto.title,
      description: dto.description,
      iconUri: dto.iconUri,
      uri: dto.uri,
    );
  }
}

class NotionPage {
  final String id;
  final String title;
  final String? icon;
  final String uri;

  NotionPage({
    required this.id,
    required this.title,
    this.icon,
    required this.uri,
  });

  static NotionPage fromDTO(NotionPageDTO dto) {
    return NotionPage(
      id: dto.id,
      title: dto.title,
      icon: dto.icon,
      uri: dto.uri,
    );
  }
}
