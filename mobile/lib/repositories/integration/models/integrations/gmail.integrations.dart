import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class GmailPropertiesDTO implements IntegrationPropertiesDTO {
  final String id;
  final String avatarUri;
  final String displayName;
  final String givenName;
  final String familyName;
  final String email;

  GmailPropertiesDTO({
    required this.id,
    required this.avatarUri,
    required this.displayName,
    required this.givenName,
    required this.familyName,
    required this.email,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'avatarUri': avatarUri,
      'displayName': displayName,
      'givenName': givenName,
      'familyName': familyName,
      'email': email,
    };
  }

  factory GmailPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return GmailPropertiesDTO(
      id: json['id'] as String,
      avatarUri: json['avatarUri'] as String,
      displayName: json['displayName'] as String,
      givenName: json['givenName'] as String,
      familyName: json['familyName'] as String,
      email: json['email'] as String,
    );
  }
}
