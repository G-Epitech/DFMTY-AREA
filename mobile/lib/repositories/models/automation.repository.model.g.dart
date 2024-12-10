// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'automation.repository.model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AutomationManifestTriggerActionProperty
    _$AutomationManifestTriggerActionPropertyFromJson(
            Map<String, dynamic> json) =>
        AutomationManifestTriggerActionProperty(
          name: json['name'] as String,
          description: json['description'] as String,
          type: json['type'] as String,
        );

Map<String, dynamic> _$AutomationManifestTriggerActionPropertyToJson(
        AutomationManifestTriggerActionProperty instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'type': instance.type,
    };

AutomationManifestTriggerAction _$AutomationManifestTriggerActionFromJson(
        Map<String, dynamic> json) =>
    AutomationManifestTriggerAction(
      name: json['name'] as String,
      description: json['description'] as String,
      icon: json['icon'] as String,
      parameters: (json['parameters'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionProperty.fromJson(
                e as Map<String, dynamic>)),
      ),
      facts: (json['facts'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerActionProperty.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationManifestTriggerActionToJson(
        AutomationManifestTriggerAction instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'icon': instance.icon,
      'parameters': instance.parameters,
      'facts': instance.facts,
    };

AutomationManifest _$AutomationManifestFromJson(Map<String, dynamic> json) =>
    AutomationManifest(
      name: json['name'] as String,
      iconUri: json['iconUri'] as String,
      color: json['color'] as String,
      triggers: (json['triggers'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerAction.fromJson(
                e as Map<String, dynamic>)),
      ),
      actions: (json['actions'] as Map<String, dynamic>).map(
        (k, e) => MapEntry(
            k,
            AutomationManifestTriggerAction.fromJson(
                e as Map<String, dynamic>)),
      ),
    );

Map<String, dynamic> _$AutomationManifestToJson(AutomationManifest instance) =>
    <String, dynamic>{
      'name': instance.name,
      'iconUri': instance.iconUri,
      'color': instance.color,
      'triggers': instance.triggers,
      'actions': instance.actions,
    };
