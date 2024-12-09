// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'integration.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetUserIntegrationDTO _$InGetUserIntegrationDTOFromJson(
        Map<String, dynamic> json) =>
    InGetUserIntegrationDTO();

Map<String, dynamic> _$InGetUserIntegrationDTOToJson(
        InGetUserIntegrationDTO instance) =>
    <String, dynamic>{};

OutGetUserIntegrationDTO _$OutGetUserIntegrationDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetUserIntegrationDTO(
      page: pageIntegrationFromJson(json['page'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetUserIntegrationDTOToJson(
        OutGetUserIntegrationDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };

OutGetIntegrationNamesDTO _$OutGetIntegrationNamesDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetIntegrationNamesDTO(
      page: pageIntegrationsNameFromJson(json['page'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetIntegrationNamesDTOToJson(
        OutGetIntegrationNamesDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };
