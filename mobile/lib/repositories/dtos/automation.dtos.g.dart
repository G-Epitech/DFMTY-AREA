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
      id: json['id'] as String,
      label: json['label'] as String,
      description: json['description'] as String,
      ownerId: json['ownerId'] as String,
      trigger:
          AutomationTrigger.fromJson(json['trigger'] as Map<String, dynamic>),
      actions: (json['actions'] as List<dynamic>)
          .map((e) => AutomationAction.fromJson(e as Map<String, dynamic>))
          .toList(),
      enabled: json['enabled'] as bool,
      updatedAt: DateTime.parse(json['updatedAt'] as String),
    );

Map<String, dynamic> _$OutGetAutomationIDDTOToJson(
        OutGetAutomationIDDTO instance) =>
    <String, dynamic>{
      'id': instance.id,
      'label': instance.label,
      'description': instance.description,
      'ownerId': instance.ownerId,
      'trigger': instance.trigger,
      'actions': instance.actions,
      'enabled': instance.enabled,
      'updatedAt': instance.updatedAt.toIso8601String(),
    };
