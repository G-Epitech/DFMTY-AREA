import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/utils/json.dart';

part 'google_oauth2.dtos.g.dart';

@JsonSerializable()
class OutGetGoogleOAuth2Configuration implements Json {
  final List<String> scopes;
  final List<Map<String, dynamic>> clientIds;
  final String endpoint;

  OutGetGoogleOAuth2Configuration({
    required this.scopes,
    required this.clientIds,
    required this.endpoint,
  });

  factory OutGetGoogleOAuth2Configuration.fromJson(Map<String, dynamic> json) =>
      _$OutGetGoogleOAuth2ConfigurationFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetGoogleOAuth2ConfigurationToJson(this);
}

@JsonSerializable()
class OutGetTokensFromGoogleOAuth2DTO {
  final String accessToken;
  final String refreshToken;

  OutGetTokensFromGoogleOAuth2DTO({
    required this.accessToken,
    required this.refreshToken,
  });

  factory OutGetTokensFromGoogleOAuth2DTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetTokensFromGoogleOAuth2DTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$OutGetTokensFromGoogleOAuth2DTOToJson(this);
}

@JsonSerializable()
class InGetTokensFromGoogleOAuth2DTO implements Json {
  final String code;
  final String provider;

  InGetTokensFromGoogleOAuth2DTO({
    required this.code,
    required this.provider,
  });

  factory InGetTokensFromGoogleOAuth2DTO.fromJson(Map<String, dynamic> json) =>
      _$InGetTokensFromGoogleOAuth2DTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetTokensFromGoogleOAuth2DTOToJson(this);
}

@JsonSerializable()
class OutGetGoogleOAuth2Credentials implements Json {
  final String accessToken;
  final String refreshToken;

  OutGetGoogleOAuth2Credentials({
    required this.accessToken,
    required this.refreshToken,
  });

  factory OutGetGoogleOAuth2Credentials.fromJson(Map<String, dynamic> json) =>
      _$OutGetGoogleOAuth2CredentialsFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetGoogleOAuth2CredentialsToJson(this);
}

@JsonSerializable()
class InGetGoogleOAuth2Credentials implements Json {
  final String refreshToken;
  final String tokenType;
  final String accessToken;

  InGetGoogleOAuth2Credentials({
    required this.refreshToken,
    required this.tokenType,
    required this.accessToken,
  });

  factory InGetGoogleOAuth2Credentials.fromJson(Map<String, dynamic> json) =>
      _$InGetGoogleOAuth2CredentialsFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetGoogleOAuth2CredentialsToJson(this);
}
