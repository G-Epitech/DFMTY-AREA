// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authentication.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InLoginDTO _$InLoginDTOFromJson(Map<String, dynamic> json) => InLoginDTO(
      email: json['email'] as String,
      password: json['password'] as String,
    );

Map<String, dynamic> _$InLoginDTOToJson(InLoginDTO instance) =>
    <String, dynamic>{
      'email': instance.email,
      'password': instance.password,
    };

OutLoginDTO _$OutLoginDTOFromJson(Map<String, dynamic> json) => OutLoginDTO(
      token: json['token'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutLoginDTOToJson(OutLoginDTO instance) =>
    <String, dynamic>{
      'token': instance.token,
      'refreshToken': instance.refreshToken,
    };

InRefreshTokenDTO _$InRefreshTokenDTOFromJson(Map<String, dynamic> json) =>
    InRefreshTokenDTO(
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$InRefreshTokenDTOToJson(InRefreshTokenDTO instance) =>
    <String, dynamic>{
      'refreshToken': instance.refreshToken,
    };

OutRefreshTokenDTO _$OutRefreshTokenDTOFromJson(Map<String, dynamic> json) =>
    OutRefreshTokenDTO(
      token: json['token'] as String,
    );

Map<String, dynamic> _$OutRefreshTokenDTOToJson(OutRefreshTokenDTO instance) =>
    <String, dynamic>{
      'token': instance.token,
    };

InRegisterDTO _$InRegisterDTOFromJson(Map<String, dynamic> json) =>
    InRegisterDTO(
      email: json['email'] as String,
      password: json['password'] as String,
      name: json['name'] as String,
    );

Map<String, dynamic> _$InRegisterDTOToJson(InRegisterDTO instance) =>
    <String, dynamic>{
      'email': instance.email,
      'password': instance.password,
      'name': instance.name,
    };

OutRegisterDTO _$OutRegisterDTOFromJson(Map<String, dynamic> json) =>
    OutRegisterDTO(
      token: json['token'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutRegisterDTOToJson(OutRegisterDTO instance) =>
    <String, dynamic>{
      'token': instance.token,
      'refreshToken': instance.refreshToken,
    };
