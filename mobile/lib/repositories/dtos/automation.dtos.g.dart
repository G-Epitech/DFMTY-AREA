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
