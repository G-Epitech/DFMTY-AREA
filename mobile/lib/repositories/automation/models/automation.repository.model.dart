import 'package:json_annotation/json_annotation.dart';

part 'automation.repository.model.g.dart';

@JsonSerializable()
class AutomationManifestTriggerActionPropertyDTO {
  final String name;
  final String description;
  final String type;

  AutomationManifestTriggerActionPropertyDTO({
    required this.name,
    required this.description,
    required this.type,
  });

  factory AutomationManifestTriggerActionPropertyDTO.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationManifestTriggerActionPropertyDTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationManifestTriggerActionPropertyDTOToJson(this);
}

@JsonSerializable()
class AutomationManifestTriggerActionDTO {
  final String name;
  final String description;
  final String icon;
  final Map<String, AutomationManifestTriggerActionPropertyDTO> parameters;
  final Map<String, AutomationManifestTriggerActionPropertyDTO> facts;

  AutomationManifestTriggerActionDTO({
    required this.name,
    required this.description,
    required this.icon,
    required this.parameters,
    required this.facts,
  });

  factory AutomationManifestTriggerActionDTO.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationManifestTriggerActionDTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationManifestTriggerActionDTOToJson(this);
}

@JsonSerializable()
class AutomationManifestDTO {
  final String name;
  final String iconUri;
  final String color;
  final Map<String, AutomationManifestTriggerActionDTO> triggers;
  final Map<String, AutomationManifestTriggerActionDTO> actions;

  AutomationManifestDTO({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.triggers,
    required this.actions,
  });

  factory AutomationManifestDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationManifestDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationManifestDTOToJson(this);
}

@JsonSerializable()
class AutomationTriggerParameterDTO {
  final String identifier;
  final String value;

  AutomationTriggerParameterDTO({
    required this.identifier,
    required this.value,
  });

  factory AutomationTriggerParameterDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationTriggerParameterDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationTriggerParameterDTOToJson(this);
}

@JsonSerializable()
class AutomationTriggerDTO {
  final String identifier;
  final List<AutomationTriggerParameterDTO> parameters;
  final List<String> providers;

  AutomationTriggerDTO({
    required this.identifier,
    required this.parameters,
    required this.providers,
  });

  factory AutomationTriggerDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationTriggerDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationTriggerDTOToJson(this);
}

@JsonSerializable()
class AutomationActionParameterDTO extends AutomationTriggerParameterDTO {
  final String type;

  AutomationActionParameterDTO({
    required super.identifier,
    required super.value,
    required this.type,
  });

  factory AutomationActionParameterDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationActionParameterDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$AutomationActionParameterDTOToJson(this);
}

@JsonSerializable()
class AutomationActionDTO {
  final String identifier;
  final List<AutomationActionParameterDTO> parameters;
  final List<String> providers;

  AutomationActionDTO({
    required this.identifier,
    required this.parameters,
    required this.providers,
  });

  factory AutomationActionDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationActionDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationActionDTOToJson(this);
}

@JsonSerializable()
class AutomationDTO {
  final String id;
  final String label;
  final String description;
  final String ownerId;
  final AutomationTriggerDTO trigger;
  final List<AutomationActionDTO> actions;
  final bool enabled;
  final DateTime updatedAt;

  AutomationDTO({
    required this.id,
    required this.label,
    required this.description,
    required this.ownerId,
    required this.trigger,
    required this.actions,
    required this.enabled,
    required this.updatedAt,
  });

  factory AutomationDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationDTOToJson(this);
}
