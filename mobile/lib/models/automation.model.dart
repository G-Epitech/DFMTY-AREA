import 'package:triggo/repositories/automation/models/automation.repository.model.dart';

class Automation {
  final String id;
  final String label;
  final String description;
  final String ownerId;
  final AutomationTrigger trigger;
  final List<AutomationAction> actions;
  final bool enabled;
  final DateTime updatedAt;
  final String iconUri;
  final int iconColor;

  const Automation({
    required this.id,
    required this.label,
    required this.description,
    required this.ownerId,
    required this.trigger,
    required this.actions,
    required this.enabled,
    required this.updatedAt,
    required this.iconUri,
    required this.iconColor,
  });

  factory Automation.fromDTO(AutomationDTO json) {
    return Automation(
      id: json.id,
      label: json.label,
      description: json.description,
      ownerId: json.ownerId,
      trigger: AutomationTrigger.fromDTO(json.trigger),
      actions: json.actions.map((e) => AutomationAction.fromDTO(e)).toList(),
      enabled: json.enabled,
      updatedAt: json.updatedAt,
      iconUri: "assets/icons/chat.svg",
      iconColor: 0xFFEE883A,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'label': label,
      'description': description,
      'ownerId': ownerId,
      'trigger': trigger.toJson(),
      'actions': actions.map((e) => e.toJson()).toList(),
      'enabled': enabled,
      'updatedAt': updatedAt.toIso8601String(),
    };
  }

  Automation copyWith({
    String? id,
    String? label,
    String? description,
    String? ownerId,
    AutomationTrigger? trigger,
    List<AutomationAction>? actions,
    bool? enabled,
    DateTime? updatedAt,
    String? iconUri,
    int? iconColor,
  }) {
    return Automation(
      id: id ?? this.id,
      label: label ?? this.label,
      description: description ?? this.description,
      ownerId: ownerId ?? this.ownerId,
      trigger: trigger ?? this.trigger,
      actions: actions ?? this.actions,
      enabled: enabled ?? this.enabled,
      updatedAt: updatedAt ?? this.updatedAt,
      iconUri: iconUri ?? this.iconUri,
      iconColor: iconColor ?? this.iconColor,
    );
  }
}

class AutomationTrigger {
  final String identifier;
  final List<AutomationTriggerParameter> parameters;
  final List<String> dependencies;

  const AutomationTrigger({
    required this.identifier,
    required this.parameters,
    required this.dependencies,
  });

  factory AutomationTrigger.fromDTO(AutomationTriggerDTO json) {
    return AutomationTrigger(
      identifier: json.identifier,
      parameters: json.parameters
          .map((e) => AutomationTriggerParameter.fromDTO(e))
          .toList(),
      dependencies: List<String>.from(json.dependencies),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'identifier': identifier,
      'parameters': parameters.map((e) => e.toJson()).toList(),
      'dependencies': dependencies,
    };
  }

  AutomationTrigger copyWith({
    String? identifier,
    List<AutomationTriggerParameter>? parameters,
    List<String>? dependencies,
  }) {
    return AutomationTrigger(
      identifier: identifier ?? this.identifier,
      parameters: parameters ?? this.parameters,
      dependencies: dependencies ?? this.dependencies,
    );
  }
}

class AutomationTriggerParameter {
  final String identifier;
  final String value;

  AutomationTriggerParameter({
    required this.identifier,
    required this.value,
  });

  factory AutomationTriggerParameter.fromDTO(
      AutomationTriggerParameterDTO json) {
    return AutomationTriggerParameter(
      identifier: json.identifier,
      value: json.value,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'identifier': identifier,
      'value': value,
    };
  }

  AutomationTriggerParameter copyWith({
    String? identifier,
    String? value,
  }) {
    return AutomationTriggerParameter(
      identifier: identifier ?? this.identifier,
      value: value ?? this.value,
    );
  }
}

class AutomationAction {
  final String identifier;
  final List<AutomationActionParameter> parameters;
  final List<String> dependencies;

  AutomationAction({
    required this.identifier,
    required this.parameters,
    required this.dependencies,
  });

  factory AutomationAction.fromDTO(AutomationActionDTO json) {
    return AutomationAction(
      identifier: json.identifier,
      parameters: json.parameters
          .map((e) => AutomationActionParameter.fromDTO(e))
          .toList(),
      dependencies: List<String>.from(json.dependencies),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'identifier': identifier,
      'parameters': parameters.map((e) => e.toJson()).toList(),
      'dependencies': dependencies,
    };
  }

  AutomationAction copyWith({
    String? identifier,
    List<AutomationActionParameter>? parameters,
    List<String>? dependencies,
  }) {
    return AutomationAction(
      identifier: identifier ?? this.identifier,
      parameters: parameters ?? this.parameters,
      dependencies: dependencies ?? this.dependencies,
    );
  }
}

class AutomationActionParameter {
  final String type;
  final String identifier;
  final String value;

  AutomationActionParameter({
    required this.type,
    required this.identifier,
    required this.value,
  });

  factory AutomationActionParameter.fromDTO(AutomationActionParameterDTO json) {
    return AutomationActionParameter(
      type: json.type,
      identifier: json.identifier,
      value: json.value,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'type': type,
      'identifier': identifier,
      'value': value,
    };
  }

  AutomationActionParameter copyWith({
    String? type,
    String? identifier,
    String? value,
  }) {
    return AutomationActionParameter(
      type: type ?? this.type,
      identifier: identifier ?? this.identifier,
      value: value ?? this.value,
    );
  }
}

class AutomationSchemas {
  final Map<String, AutomationSchema> schemas;

  AutomationSchemas({
    required this.schemas,
  });

  factory AutomationSchemas.fromDTO(Map<String, AutomationSchemaDTO> json) {
    return AutomationSchemas(
      schemas: json
          .map((key, value) => MapEntry(key, AutomationSchema.fromDTO(value))),
    );
  }
}

class AutomationSchema {
  final String name;
  final String iconUri;
  final String color;
  final Map<String, AutomationSchemaTriggerAction> triggers;
  final Map<String, AutomationSchemaTriggerAction> actions;

  AutomationSchema({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.triggers,
    required this.actions,
  });

  factory AutomationSchema.fromDTO(AutomationSchemaDTO json) {
    return AutomationSchema(
      name: json.name,
      iconUri: json.iconUri,
      color: json.color,
      triggers: json.triggers.map((key, value) =>
          MapEntry(key, AutomationSchemaTriggerAction.fromDTO(value))),
      actions: json.actions.map((key, value) =>
          MapEntry(key, AutomationSchemaTriggerAction.fromDTO(value))),
    );
  }
}

class AutomationSchemaTriggerAction {
  final String name;
  final String description;
  final String icon;
  final Map<String, AutomationSchemaTriggerActionProperty> parameters;
  final Map<String, AutomationSchemaTriggerActionProperty> facts;
  final Map<String, AutomationSchemaDependenciesProperty> dependencies;

  AutomationSchemaTriggerAction({
    required this.name,
    required this.description,
    required this.icon,
    required this.parameters,
    required this.facts,
    required this.dependencies,
  });

  factory AutomationSchemaTriggerAction.fromDTO(
      AutomationSchemaTriggerActionDTO json) {
    return AutomationSchemaTriggerAction(
      name: json.name,
      description: json.description,
      icon: json.icon,
      parameters: json.parameters.map((key, value) =>
          MapEntry(key, AutomationSchemaTriggerActionProperty.fromDTO(value))),
      facts: json.facts.map((key, value) =>
          MapEntry(key, AutomationSchemaTriggerActionProperty.fromDTO(value))),
      dependencies: json.dependencies.map((key, value) =>
          MapEntry(key, AutomationSchemaDependenciesProperty.fromDTO(value))),
    );
  }
}

class AutomationSchemaTriggerActionProperty {
  final String name;
  final String description;
  final String type;

  AutomationSchemaTriggerActionProperty({
    required this.name,
    required this.description,
    required this.type,
  });

  factory AutomationSchemaTriggerActionProperty.fromDTO(
      AutomationSchemaTriggerActionPropertyDTO json) {
    return AutomationSchemaTriggerActionProperty(
      name: json.name,
      description: json.description,
      type: json.type,
    );
  }
}

class AutomationSchemaDependenciesProperty {
  final String require;
  final bool optional;

  AutomationSchemaDependenciesProperty({
    required this.require,
    required this.optional,
  });

  factory AutomationSchemaDependenciesProperty.fromDTO(
      AutomationSchemaDependenciesPropertyDTO json) {
    return AutomationSchemaDependenciesProperty(
      require: json.require,
      optional: json.optional,
    );
  }
}
