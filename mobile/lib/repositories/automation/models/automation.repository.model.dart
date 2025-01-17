import 'package:json_annotation/json_annotation.dart';

part 'automation.repository.model.g.dart';

@JsonSerializable()
class AutomationSchemaTriggerActionPropertyDTO {
  final String name;
  final String description;
  final String type;

  AutomationSchemaTriggerActionPropertyDTO({
    required this.name,
    required this.description,
    required this.type,
  });

  factory AutomationSchemaTriggerActionPropertyDTO.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationSchemaTriggerActionPropertyDTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationSchemaTriggerActionPropertyDTOToJson(this);
}

@JsonSerializable()
class AutomationSchemaDependenciesPropertyDTO {
  final String require;
  final bool optional;

  AutomationSchemaDependenciesPropertyDTO({
    required this.require,
    required this.optional,
  });

  factory AutomationSchemaDependenciesPropertyDTO.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationSchemaDependenciesPropertyDTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationSchemaDependenciesPropertyDTOToJson(this);
}

@JsonSerializable()
class AutomationSchemaTriggerActionDTO {
  final String name;
  final String description;
  final String icon;
  final Map<String, AutomationSchemaTriggerActionPropertyDTO> parameters;
  final Map<String, AutomationSchemaTriggerActionPropertyDTO> facts;
  final Map<String, AutomationSchemaDependenciesPropertyDTO> dependencies;

  AutomationSchemaTriggerActionDTO({
    required this.name,
    required this.description,
    required this.icon,
    required this.parameters,
    required this.facts,
    required this.dependencies,
  });

  factory AutomationSchemaTriggerActionDTO.fromJson(
          Map<String, dynamic> json) =>
      _$AutomationSchemaTriggerActionDTOFromJson(json);

  Map<String, dynamic> toJson() =>
      _$AutomationSchemaTriggerActionDTOToJson(this);
}

@JsonSerializable()
class AutomationSchemaDTO {
  final String name;
  final String iconUri;
  final String color;
  final Map<String, AutomationSchemaTriggerActionDTO> triggers;
  final Map<String, AutomationSchemaTriggerActionDTO> actions;

  AutomationSchemaDTO({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.triggers,
    required this.actions,
  });

  factory AutomationSchemaDTO.fromJson(Map<String, dynamic> json) =>
      _$AutomationSchemaDTOFromJson(json);

  Map<String, dynamic> toJson() => _$AutomationSchemaDTOToJson(this);
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
