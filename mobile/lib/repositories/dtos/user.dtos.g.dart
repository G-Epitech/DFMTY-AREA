// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetUserDTO _$InGetUserDTOFromJson(Map<String, dynamic> json) =>
    InGetUserDTO();

Map<String, dynamic> _$InGetUserDTOToJson(InGetUserDTO instance) =>
    <String, dynamic>{};

OutGetUserDTO _$OutGetUserDTOFromJson(Map<String, dynamic> json) =>
    OutGetUserDTO(
      email: json['email'] as String,
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
      picture: json['picture'] as String,
    );

Map<String, dynamic> _$OutGetUserDTOToJson(OutGetUserDTO instance) =>
    <String, dynamic>{
      'email': instance.email,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'picture': instance.picture,
    };
