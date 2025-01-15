import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/utils/json.dart';

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

class NotionDatabaseDTO implements Json {
  final String id;
  final String title;
  final String? description;
  final String? iconUri;
  final String uri;

  NotionDatabaseDTO({
    required this.id,
    required this.title,
    this.description,
    this.iconUri,
    required this.uri,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'description': description,
      'iconUri': iconUri,
      'uri': uri,
    };
  }

  factory NotionDatabaseDTO.fromJson(Map<String, dynamic> json) {
    return NotionDatabaseDTO(
      id: json['id'] as String,
      title: json['title'] as String,
      description: json['description'] as String?,
      iconUri: json['iconUri'] as String?,
      uri: json['uri'] as String,
    );
  }
}

class NotionPageDTO implements Json {
  final String id;
  final String title;
  final String? icon;
  final String uri;

  NotionPageDTO({
    required this.id,
    required this.title,
    this.icon,
    required this.uri,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'icon': icon,
      'uri': uri,
    };
  }

  factory NotionPageDTO.fromJson(Map<String, dynamic> json) {
    return NotionPageDTO(
      id: json['id'] as String,
      title: json['title'] as String,
      icon: json['icon'] as String?,
      uri: json['uri'] as String,
    );
  }
}
