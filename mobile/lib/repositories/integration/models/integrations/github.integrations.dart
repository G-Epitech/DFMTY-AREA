import 'package:triggo/repositories/integration/models/integration.repository.model.dart';

class GithubPropertiesDTO implements IntegrationPropertiesDTO {
  final int id;
  final String name;
  final String? email;
  final String? bio;
  final String avatarUri;
  final String profileUri;
  final String? company;
  final String? blog;
  final String? location;
  final int followers;
  final int following;

  GithubPropertiesDTO({
    required this.id,
    required this.name,
    this.email,
    this.bio,
    required this.avatarUri,
    required this.profileUri,
    this.company,
    this.blog,
    this.location,
    required this.followers,
    required this.following,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'email': email,
      'bio': bio,
      'avatarUri': avatarUri,
      'profileUri': profileUri,
      'company': company,
      'blog': blog,
      'location': location,
      'followers': followers,
      'following': following,
    };
  }

  factory GithubPropertiesDTO.fromJson(Map<String, dynamic> json) {
    return GithubPropertiesDTO(
      id: json['id'] as int,
      name: json['name'] as String,
      email: json['email'] as String?,
      bio: json['bio'] as String?,
      avatarUri: json['avatarUri'] as String,
      profileUri: json['profileUri'] as String,
      company: json['company'] as String?,
      blog: json['blog'] as String?,
      location: json['location'] as String?,
      followers: json['followers'] as int,
      following: json['following'] as int,
    );
  }
}
