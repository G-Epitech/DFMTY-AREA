// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'automation.repository.model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AutomationSchemaTriggerActionPropertyDTO
    _$AutomationSchemaTriggerActionPropertyDTOFromJson(
            Map<String, dynamic> json) =>
        AutomationSchemaTriggerActionPropertyDTO(
          name: json['name'] as String,
          description: json['description'] as String,
          type: json['type'] as String,
        );

Map<String, dynamic> _$AutomationSchemaTriggerActionPropertyDTOToJson(
        AutomationSchemaTriggerActionPropertyDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'type': instance.type,
    };

AutomationSchemaDependenciesPropertyDTO
    _$AutomationSchemaDependenciesPropertyDTOFromJson(
            Map<String, dynamic> json) =>
        AutomationSchemaDependenciesPropertyDTO(
          require: json['require'] as String,
          optional: json['optional'] as bool,
        );

Map<String, dynamic> _$AutomationSchemaDependenciesPropertyDTOToJson(
        AutomationSchemaDependenciesPropertyDTO instance) =>
    <String, dynamic>{
      'require': instance.require,
      'optional': instance.optional,
    };

AutomationSchemaTriggerActionDTO _$AutomationSchemaTriggerActionDTOFromJson(
        Map<String, dynamic> json) =>
    AutomationSchemaTriggerActionDTO(
      name: json['name'] as String,
      description: json['description'] as String,
      icon: json['icon'] as String,
      parameters: (json['parameters'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationSchemaTriggerActionPropertyDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
      facts: (json['facts'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationSchemaTriggerActionPropertyDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
      dependencies: (json['dependencies'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationSchemaDependenciesPropertyDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationSchemaTriggerActionDTOToJson(
        AutomationSchemaTriggerActionDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'icon': instance.icon,
      'parameters': instance.parameters,
      'facts': instance.facts,
      'dependencies': instance.dependencies,
    };

AutomationSchemaDTO _$AutomationSchemaDTOFromJson(Map<String, dynamic> json) =>
    AutomationSchemaDTO(
      name: json['name'] as String,
      iconUri: json['iconUri'] as String,
      color: json['color'] as String,
      triggers: (json['triggers'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationSchemaTriggerActionDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
      actions: (json['actions'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationSchemaTriggerActionDTO.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationSchemaDTOToJson(
        AutomationSchemaDTO instance) =>
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
      dependencies: (json['dependencies'] as List<dynamic>)
          .map((e) => e as String)
          .toList(),
    );

Map<String, dynamic> _$AutomationTriggerDTOToJson(
        AutomationTriggerDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'parameters': instance.parameters,
      'dependencies': instance.dependencies,
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
      dependencies: (json['dependencies'] as List<dynamic>)
          .map((e) => e as String)
          .toList(),
    );

Map<String, dynamic> _$AutomationActionDTOToJson(
        AutomationActionDTO instance) =>
    <String, dynamic>{
      'identifier': instance.identifier,
      'parameters': instance.parameters,
      'dependencies': instance.dependencies,
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
      iconUri: json['iconUri'] as String,
      iconColor: json['iconColor'] as String,
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
      'iconUri': instance.iconUri,
      'iconColor': instance.iconColor,
    };
