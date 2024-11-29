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
      accessToken: json['accessToken'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutLoginDTOToJson(OutLoginDTO instance) =>
    <String, dynamic>{
      'accessToken': instance.accessToken,
      'refreshToken': instance.refreshToken,
    };

OutRefreshTokenDTO _$OutRefreshTokenDTOFromJson(Map<String, dynamic> json) =>
    OutRefreshTokenDTO(
      accessToken: json['accessToken'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutRefreshTokenDTOToJson(OutRefreshTokenDTO instance) =>
    <String, dynamic>{
      'accessToken': instance.accessToken,
      'refreshToken': instance.refreshToken,
    };

InRegisterDTO _$InRegisterDTOFromJson(Map<String, dynamic> json) =>
    InRegisterDTO(
      email: json['email'] as String,
      password: json['password'] as String,
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
    );

Map<String, dynamic> _$InRegisterDTOToJson(InRegisterDTO instance) =>
    <String, dynamic>{
      'email': instance.email,
      'password': instance.password,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
    };

OutRegisterDTO _$OutRegisterDTOFromJson(Map<String, dynamic> json) =>
    OutRegisterDTO(
      accessToken: json['accessToken'] as String,
      refreshToken: json['refreshToken'] as String,
    );

Map<String, dynamic> _$OutRegisterDTOToJson(OutRegisterDTO instance) =>
    <String, dynamic>{
      'accessToken': instance.accessToken,
      'refreshToken': instance.refreshToken,
    };
