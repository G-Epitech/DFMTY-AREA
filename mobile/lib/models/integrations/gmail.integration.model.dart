import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/gmail.integrations.dart';

class GmailIntegration extends Integration {
  final String displayName;
  final String firstName;
  final String lastName;
  final String email;
  final String avatarUri;

  GmailIntegration({
    required super.name,
    required super.id,
    required this.displayName,
    required this.firstName,
    required this.lastName,
    required this.email,
    required this.avatarUri,
  });

  static GmailIntegration fromDTO(IntegrationDTO dto) {
    GmailPropertiesDTO properties = dto.properties as GmailPropertiesDTO;
    return GmailIntegration(
      name: 'Gmail',
      id: dto.id,
      displayName: properties.displayName,
      firstName: properties.firstName,
      lastName: properties.lastName,
      email: properties.email,
      avatarUri: properties.avatarUri,
    );
  }
}
