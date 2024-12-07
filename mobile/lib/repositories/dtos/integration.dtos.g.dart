// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'integration.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetIntegrationDTO _$InGetIntegrationDTOFromJson(Map<String, dynamic> json) =>
    InGetIntegrationDTO();

Map<String, dynamic> _$InGetIntegrationDTOToJson(
        InGetIntegrationDTO instance) =>
    <String, dynamic>{};

OutGetIntegrationDTO _$OutGetIntegrationDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetIntegrationDTO(
      page: pageIntegrationFromJson(json['page'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetIntegrationDTOToJson(
        OutGetIntegrationDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };
