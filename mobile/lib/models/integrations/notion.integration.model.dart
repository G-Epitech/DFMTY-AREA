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
