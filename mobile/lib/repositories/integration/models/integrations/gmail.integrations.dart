import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class GmailPropertiesDTO implements IntegrationPropertiesDTO {
  final String id;
  final String avatarUri;
  final String displayName;
  final String firstName;
  final String lastName;
  final String email;

  GmailPropertiesDTO({
    required this.id,
    required this.avatarUri,
    required this.displayName,
    required this.firstName,
    required this.lastName,
    required this.email,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'avatarUri': avatarUri,
      'displayName': displayName,
      'firstName': firstName,
      'lastName': lastName,
      'email': email,
    };
  }

  factory GmailPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return GmailPropertiesDTO(
      id: json['id'] as String,
      avatarUri: json['avatarUri'] as String,
      displayName: json['displayName'] as String,
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
      email: json['email'] as String,
    );
  }
}
