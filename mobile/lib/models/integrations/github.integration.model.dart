import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/integration/models/integrations/github.integrations.dart';

class GithubIntegration extends Integration {
  final String? username;
  final String? email;
  final String avatarUri;
  final int followers;
  final int following;

  GithubIntegration({
    required super.id,
    required super.name,
    required this.username,
    required this.email,
    required this.avatarUri,
    required this.followers,
    required this.following,
  });

  static GithubIntegration fromDTO(IntegrationDTO dto) {
    GithubPropertiesDTO properties = dto.properties as GithubPropertiesDTO;
    return GithubIntegration(
      name: 'Github',
      id: dto.id,
      username: properties.name,
      email: properties.email,
      avatarUri: properties.avatarUri,
      followers: properties.followers,
      following: properties.following,
    );
  }
}
