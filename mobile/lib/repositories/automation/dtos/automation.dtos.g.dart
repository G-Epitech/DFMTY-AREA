// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'automation.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetAutomationSchemaDTO _$InGetAutomationSchemaDTOFromJson(
        Map<String, dynamic> json) =>
    InGetAutomationSchemaDTO();

Map<String, dynamic> _$InGetAutomationSchemaDTOToJson(
        InGetAutomationSchemaDTO instance) =>
    <String, dynamic>{};

OutGetAutomationIDDTO _$OutGetAutomationIDDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetAutomationIDDTO(
      automation:
          AutomationDTO.fromJson(json['automation'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetAutomationIDDTOToJson(
        OutGetAutomationIDDTO instance) =>
    <String, dynamic>{
      'automation': instance.automation,
    };

Map<String, dynamic> _$OutGetAutomationsDTOToJson(
        OutGetAutomationsDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };

InPostAutomationDTO _$InPostAutomationDTOFromJson(Map<String, dynamic> json) =>
    InPostAutomationDTO(
      automation:
          AutomationDTO.fromJson(json['automation'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$InPostAutomationDTOToJson(
        InPostAutomationDTO instance) =>
    <String, dynamic>{
      'automation': instance.automation,
    };

OutPostAutomationDTO _$OutPostAutomationDTOFromJson(
        Map<String, dynamic> json) =>
    OutPostAutomationDTO();

Map<String, dynamic> _$OutPostAutomationDTOToJson(
        OutPostAutomationDTO instance) =>
    <String, dynamic>{};
