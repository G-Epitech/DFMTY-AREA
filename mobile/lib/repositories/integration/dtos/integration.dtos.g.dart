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

Map<String, dynamic> _$OutGetUserIntegrationDTOToJson(
        OutGetUserIntegrationDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };

OutGetIntegrationURIDTO _$OutGetIntegrationURIDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetIntegrationURIDTO(
      uri: json['uri'] as String,
    );

Map<String, dynamic> _$OutGetIntegrationURIDTOToJson(
        OutGetIntegrationURIDTO instance) =>
    <String, dynamic>{
      'uri': instance.uri,
    };

OutGetUserIntegrationByIdDTO _$OutGetUserIntegrationByIdDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetUserIntegrationByIdDTO(
      integration:
          IntegrationDTO.fromJson(json['integration'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetUserIntegrationByIdDTOToJson(
        OutGetUserIntegrationByIdDTO instance) =>
    <String, dynamic>{
      'integration': instance.integration,
    };
