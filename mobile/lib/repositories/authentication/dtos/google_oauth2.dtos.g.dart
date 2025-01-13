// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'google_oauth2.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OutGetGoogleOAuth2Configuration _$OutGetGoogleOAuth2ConfigurationFromJson(
        Map<String, dynamic> json) =>
    OutGetGoogleOAuth2Configuration(
      scopes:
          (json['scopes'] as List<dynamic>).map((e) => e as String).toList(),
      clientIds: (json['clientIds'] as List<dynamic>)
          .map((e) => e as Map<String, dynamic>)
          .toList(),
      endpoint: json['endpoint'] as String,
    );

Map<String, dynamic> _$OutGetGoogleOAuth2ConfigurationToJson(
        OutGetGoogleOAuth2Configuration instance) =>
    <String, dynamic>{
      'scopes': instance.scopes,
      'clientIds': instance.clientIds,
      'endpoint': instance.endpoint,
    };

OutGetTokensFromGoogleOAuth2DTO _$OutGetTokensFromGoogleOAuth2DTOFromJson(
        Map<String, dynamic> json) =>
    OutGetTokensFromGoogleOAuth2DTO(
      accessToken: json['accessToken'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutGetTokensFromGoogleOAuth2DTOToJson(
        OutGetTokensFromGoogleOAuth2DTO instance) =>
    <String, dynamic>{
      'accessToken': instance.accessToken,
      'refreshToken': instance.refreshToken,
    };

InGetTokensFromGoogleOAuth2DTO _$InGetTokensFromGoogleOAuth2DTOFromJson(
        Map<String, dynamic> json) =>
    InGetTokensFromGoogleOAuth2DTO(
      code: json['code'] as String,
      provider: json['provider'] as String,
    );

Map<String, dynamic> _$InGetTokensFromGoogleOAuth2DTOToJson(
        InGetTokensFromGoogleOAuth2DTO instance) =>
    <String, dynamic>{
      'code': instance.code,
      'provider': instance.provider,
    };

OutGetGoogleOAuth2Credentials _$OutGetGoogleOAuth2CredentialsFromJson(
        Map<String, dynamic> json) =>
    OutGetGoogleOAuth2Credentials(
      accessToken: json['accessToken'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutGetGoogleOAuth2CredentialsToJson(
        OutGetGoogleOAuth2Credentials instance) =>
    <String, dynamic>{
      'accessToken': instance.accessToken,
      'refreshToken': instance.refreshToken,
    };

InGetGoogleOAuth2Credentials _$InGetGoogleOAuth2CredentialsFromJson(
        Map<String, dynamic> json) =>
    InGetGoogleOAuth2Credentials(
      refreshToken: json['refreshToken'] as String,
      tokenType: json['tokenType'] as String,
      accessToken: json['accessToken'] as String,
    );

Map<String, dynamic> _$InGetGoogleOAuth2CredentialsToJson(
        InGetGoogleOAuth2Credentials instance) =>
    <String, dynamic>{
      'refreshToken': instance.refreshToken,
      'tokenType': instance.tokenType,
      'accessToken': instance.accessToken,
    };
