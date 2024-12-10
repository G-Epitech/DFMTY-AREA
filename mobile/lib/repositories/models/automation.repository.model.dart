import 'package:json_annotation/json_annotation.dart';

part 'automation.repository.model.g.dart';

@JsonSerializable()
class AutomationManifestTriggerActionProperty {
  final String name;
  final String description;
  final String type;

  AutomationManifestTriggerActionProperty({
    required this.name,
    required this.description,
    required this.type,
  });

  factory AutomationManifestTriggerActionProperty.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationManifestTriggerActionPropertyFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationManifestTriggerActionPropertyToJson(this);
}

@JsonSerializable()
class AutomationManifestTriggerAction {
  final String name;
  final String description;
  final String icon;
  final Map<String, AutomationManifestTriggerActionProperty> parameters;
  final Map<String, AutomationManifestTriggerActionProperty> facts;

  AutomationManifestTriggerAction({
    required this.name,
    required this.description,
    required this.icon,
    required this.parameters,
    required this.facts,
  });

  factory AutomationManifestTriggerAction.fromJson(Map<String, dynamic> json) =>
      _$AutomationManifestTriggerActionFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationManifestTriggerActionToJson(this);
}

@JsonSerializable()
class AutomationManifest {
  final String name;
  final String iconUri;
  final String color;
  final Map<String, AutomationManifestTriggerAction> triggers;
  final Map<String, AutomationManifestTriggerAction> actions;

  AutomationManifest({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.triggers,
    required this.actions,
  });

  factory AutomationManifest.fromJson(Map<String, dynamic> json) =>
      _$AutomationManifestFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationManifestToJson(this);
}
