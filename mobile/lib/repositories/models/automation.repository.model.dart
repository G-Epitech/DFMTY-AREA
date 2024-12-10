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

@JsonSerializable()
class AutomationTriggerParameter {
  final String identifier;
  final String value;

  AutomationTriggerParameter({
    required this.identifier,
    required this.value,
  });

  factory AutomationTriggerParameter.fromJson(Map<String, dynamic> json) =>
      _$AutomationTriggerParameterFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationTriggerParameterToJson(this);
}

@JsonSerializable()
class AutomationTrigger {
  final String identifier;
  final List<AutomationTriggerParameter> parameters;
  final List<String> providers;

  AutomationTrigger({
    required this.identifier,
    required this.parameters,
    required this.providers,
  });

  factory AutomationTrigger.fromJson(Map<String, dynamic> json) =>
      _$AutomationTriggerFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationTriggerToJson(this);
}

@JsonSerializable()
class AutomationActionParameter extends AutomationTriggerParameter {
  final String type;

  AutomationActionParameter({
    required super.identifier,
    required super.value,
    required this.type,
  });

  factory AutomationActionParameter.fromJson(Map<String, dynamic> json) =>
      _$AutomationActionParameterFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$AutomationActionParameterToJson(this);
}

@JsonSerializable()
class AutomationAction {
  final String identifier;
  final List<AutomationActionParameter> parameters;
  final List<String> providers;

  AutomationAction({
    required this.identifier,
    required this.parameters,
    required this.providers,
  });

  factory AutomationAction.fromJson(Map<String, dynamic> json) =>
      _$AutomationActionFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationActionToJson(this);
}
