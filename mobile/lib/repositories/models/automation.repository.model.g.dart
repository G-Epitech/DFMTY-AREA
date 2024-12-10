// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'automation.repository.model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AutomationManifestTriggerActionPropertyDTO
    _$AutomationManifestTriggerActionPropertyDTOFromJson(
            Map<String, dynamic> json) =>
        AutomationManifestTriggerActionPropertyDTO(
          name: json['name'] as String,
          description: json['description'] as String,
          type: json['type'] as String,
        );

Map<String, dynamic> _$AutomationManifestTriggerActionPropertyDTOToJson(
        AutomationManifestTriggerActionPropertyDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'type': instance.type,
    };

AutomationManifestTriggerActionDTO _$AutomationManifestTriggerActionDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationManifestTriggerActionDTO(
      name: json['name'] as String,
      description: json['description'] as String,
      icon: json['icon'] as String,
      parameters: (json['parameters'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionPropertyDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
      facts: (json['facts'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionPropertyDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationManifestTriggerActionDTOToJson(
        AutomationManifestTriggerActionDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'icon': instance.icon,
      'parameters': instance.parameters,
      'facts': instance.facts,
    };

AutomationManifestDTO _$AutomationManifestDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationManifestDTO(
      name: json['name'] as String,
      iconUri: json['iconUri'] as String,
      color: json['color'] as String,
      triggers: (json['triggers'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
      actions: (json['actions'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationManifestDTOToJson(
        AutomationManifestDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'iconUri': instance.iconUri,
      'color': instance.color,
      'triggers': instance.triggers,
      'actions': instance.actions,
    };

AutomationTriggerParameterDTO _$AutomationTriggerParameterDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationTriggerParameterDTO(
      identifier: json['identifier'] as String,
      value: json['value'] as String,
    );

Map<String, dynamic> _$AutomationTriggerParameterDTOToJson(
        AutomationTriggerParameterDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'value': instance.value,
    };

AutomationTriggerDTO _$AutomationTriggerDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationTriggerDTO(
      identifier: json['identifier'] as String,
      parameters: (json['parameters'] as List<dynamic>)
          .map((e) =>
              AutomationTriggerParameterDTO.fromJson(e as Map<String, dynamic>))
          .toList(),
      providers:
          (json['providers'] as List<dynamic>).map((e) => e as String).toList(),
    );

Map<String, dynamic> _$AutomationTriggerDTOToJson(
        AutomationTriggerDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'parameters': instance.parameters,
      'providers': instance.providers,
    };

AutomationActionParameterDTO _$AutomationActionParameterDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationActionParameterDTO(
      identifier: json['identifier'] as String,
      value: json['value'] as String,
      type: json['type'] as String,
    );

Map<String, dynamic> _$AutomationActionParameterDTOToJson(
        AutomationActionParameterDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'value': instance.value,
      'type': instance.type,
    };

AutomationActionDTO _$AutomationActionDTOFromJson(Map<String, dynamic> json) =>
    AutomationActionDTO(
      identifier: json['identifier'] as String,
      parameters: (json['parameters'] as List<dynamic>)
          .map((e) =>
              AutomationActionParameterDTO.fromJson(e as Map<String, dynamic>))
          .toList(),
      providers:
          (json['providers'] as List<dynamic>).map((e) => e as String).toList(),
    );

Map<String, dynamic> _$AutomationActionDTOToJson(
        AutomationActionDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'parameters': instance.parameters,
      'providers': instance.providers,
    };

AutomationDTO _$AutomationDTOFromJson(Map<String, dynamic> json) =>
    AutomationDTO(
      id: json['id'] as String,
      label: json['label'] as String,
      description: json['description'] as String,
      ownerId: json['ownerId'] as String,
      trigger: AutomationTriggerDTO.fromJson(
          json['trigger'] as Map<String, dynamic>),
      actions: (json['actions'] as List<dynamic>)
          .map((e) => AutomationActionDTO.fromJson(e as Map<String, dynamic>))
          .toList(),
      enabled: json['enabled'] as bool,
      updatedAt: DateTime.parse(json['updatedAt'] as String),
    );

Map<String, dynamic> _$AutomationDTOToJson(AutomationDTO instance) =>
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
