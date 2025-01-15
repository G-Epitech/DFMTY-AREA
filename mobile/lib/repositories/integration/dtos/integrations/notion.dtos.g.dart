// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'notion.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetUserIntegrationNotionDatabasesDTO
    _$InGetUserIntegrationNotionDatabasesDTOFromJson(
            Map<String, dynamic> json) =>
        InGetUserIntegrationNotionDatabasesDTO();

Map<String, dynamic> _$InGetUserIntegrationNotionDatabasesDTOToJson(
        InGetUserIntegrationNotionDatabasesDTO instance) =>
    <String, dynamic>{};

OutGetUserIntegrationNotionDatabasesDTO
    _$OutGetUserIntegrationNotionDatabasesDTOFromJson(
            Map<String, dynamic> json) =>
        OutGetUserIntegrationNotionDatabasesDTO(
          list: (json['list'] as List<dynamic>)
              .map((e) => NotionDatabaseDTO.fromJson(e as Map<String, dynamic>))
              .toList(),
        );

Map<String, dynamic> _$OutGetUserIntegrationNotionDatabasesDTOToJson(
        OutGetUserIntegrationNotionDatabasesDTO instance) =>
    <String, dynamic>{
      'list': instance.list,
    };

InGetUserIntegrationNotionPagesDTO _$InGetUserIntegrationNotionPagesDTOFromJson(
        Map<String, dynamic> json) =>
    InGetUserIntegrationNotionPagesDTO(
      guildId: json['guildId'] as String,
    );

Map<String, dynamic> _$InGetUserIntegrationNotionPagesDTOToJson(
        InGetUserIntegrationNotionPagesDTO instance) =>
    <String, dynamic>{
      'guildId': instance.guildId,
    };

OutGetUserIntegrationNotionPagesDTO
    _$OutGetUserIntegrationNotionPagesDTOFromJson(Map<String, dynamic> json) =>
        OutGetUserIntegrationNotionPagesDTO(
          list: (json['list'] as List<dynamic>)
              .map((e) => NotionPageDTO.fromJson(e as Map<String, dynamic>))
              .toList(),
        );

Map<String, dynamic> _$OutGetUserIntegrationNotionPagesDTOToJson(
        OutGetUserIntegrationNotionPagesDTO instance) =>
    <String, dynamic>{
      'list': instance.list,
    };
