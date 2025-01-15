import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class OpenAIPropertiesDTO implements IntegrationPropertiesDTO {
  final String ownerId;
  final String ownerName;
  final String ownerEmail;

  OpenAIPropertiesDTO({
    required this.ownerId,
    required this.ownerName,
    required this.ownerEmail,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'ownerId': ownerId,
      'ownerName': ownerName,
      'ownerEmail': ownerEmail,
    };
  }

  factory OpenAIPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return OpenAIPropertiesDTO(
      ownerId: json['ownerId'] as String,
      ownerName: json['ownerName'] as String,
      ownerEmail: json['ownerEmail'] as String,
    );
  }
}
