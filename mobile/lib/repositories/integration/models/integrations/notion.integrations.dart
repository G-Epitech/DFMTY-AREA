import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class NotionPropertiesDTO implements IntegrationPropertiesDTO {
  final String id;
  final String avatarUri;
  final String name;
  final String email;
  final String workspaceName;

  NotionPropertiesDTO({
    required this.id,
    required this.avatarUri,
    required this.name,
    required this.email,
    required this.workspaceName,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'avatarUri': avatarUri,
      'name': name,
      'email': email,
      'workspaceName': workspaceName,
    };
  }

  factory NotionPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return NotionPropertiesDTO(
      id: json['id'] as String,
      avatarUri: json['avatarUri'] as String,
      name: json['name'] as String,
      email: json['email'] as String,
      workspaceName: json['workspaceName'] as String,
    );
  }
}
