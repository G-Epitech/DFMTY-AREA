import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/openAI.integrations.dart';

class OpenAIIntegration extends Integration {
  final String ownerName;
  final String ownerEmail;

  OpenAIIntegration({
    required super.id,
    required super.name,
    required this.ownerName,
    required this.ownerEmail,
  });

  static OpenAIIntegration fromDTO(IntegrationDTO dto) {
    OpenAIPropertiesDTO properties = dto.properties as OpenAIPropertiesDTO;
    return OpenAIIntegration(
      name: 'OpenAi',
      id: dto.id,
      ownerName: properties.ownerName,
      ownerEmail: properties.ownerEmail,
    );
  }
}
