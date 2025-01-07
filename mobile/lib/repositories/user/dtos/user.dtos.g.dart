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
      id: json['id'] as String,
      firstName: json['firstName'] as String,
      lastName: json['lastName'] as String,
      email: json['email'] as String,
      picture: json['picture'] as String,
    );

Map<String, dynamic> _$OutGetUserDTOToJson(OutGetUserDTO instance) =>
    <String, dynamic>{
      'id': instance.id,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'email': instance.email,
      'picture': instance.picture,
    };
