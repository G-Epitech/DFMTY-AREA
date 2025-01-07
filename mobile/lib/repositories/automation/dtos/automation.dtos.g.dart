// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'automation.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetAutomationManifestDTO _$InGetAutomationManifestDTOFromJson(
        Map<String, dynamic> json) =>
    InGetAutomationManifestDTO();

Map<String, dynamic> _$InGetAutomationManifestDTOToJson(
        InGetAutomationManifestDTO instance) =>
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

OutGetAutomationsDTO _$OutGetAutomationsDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetAutomationsDTO(
      page: pageAutomationsFromJson(json['page'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$OutGetAutomationsDTOToJson(
        OutGetAutomationsDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };

InPostAutomationDTO _$InPostAutomationDTOFromJson(Map<String, dynamic> json) =>
    InPostAutomationDTO();

Map<String, dynamic> _$InPostAutomationDTOToJson(
        InPostAutomationDTO instance) =>
    <String, dynamic>{};

OutPostAutomationDTO _$OutPostAutomationDTOFromJson(
        Map<String, dynamic> json) =>
    OutPostAutomationDTO();

Map<String, dynamic> _$OutPostAutomationDTOToJson(
        OutPostAutomationDTO instance) =>
    <String, dynamic>{};
