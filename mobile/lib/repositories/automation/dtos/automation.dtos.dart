import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/automation/models/automation.repository.model.dart';
import 'package:triggo/repositories/page/models/page.repository.model.dart';
import 'package:triggo/repositories/page/utils/page_serializer.utils.dart';
import 'package:triggo/utils/json.dart';

part 'automation.dtos.g.dart';

@JsonSerializable()
class InGetAutomationSchemaDTO implements Json {
  InGetAutomationSchemaDTO();

  @override
  factory InGetAutomationSchemaDTO.fromJson(Map<String, dynamic> json) =>
      _$InGetAutomationSchemaDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetAutomationSchemaDTOToJson(this);
}

class OutGetAutomationSchemaDTO {
  final Map<String, AutomationSchemaDTO> schema;

  OutGetAutomationSchemaDTO({
    required this.schema,
  });

  factory OutGetAutomationSchemaDTO.fromJson(Map<String, dynamic> json) {
    return OutGetAutomationSchemaDTO(
      schema: json.map(
        (key, value) => MapEntry(
          key,
          AutomationSchemaDTO.fromJson(value as Map<String, dynamic>),
        ),
      ),
    );
  }

  Map<String, dynamic> toJson() {
    return schema.map(
      (key, value) => MapEntry(key, value.toJson()),
    );
  }
}

@JsonSerializable()
class OutGetAutomationIDDTO {
  final AutomationDTO automation;

  OutGetAutomationIDDTO({
    required this.automation,
  });

  factory OutGetAutomationIDDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetAutomationIDDTOFromJson({'automation': json});

  Map<String, dynamic> toJson() => _$OutGetAutomationIDDTOToJson(this);
}

Page<AutomationDTO> pageAutomationsFromJson(Map<String, dynamic> json) {
  return pageFromJson<AutomationDTO>(json, AutomationDTO.fromJson);
}

@JsonSerializable(createFactory: false)
class OutGetAutomationsDTO implements PageJson<AutomationDTO> {
  @JsonKey(fromJson: pageAutomationsFromJson, toJson: pageToJson)
  @override
  final Page<AutomationDTO> page;

  OutGetAutomationsDTO({required this.page});

  factory OutGetAutomationsDTO.fromJson(Map<String, dynamic> json) {
    return OutGetAutomationsDTO(
      page: PageJson.fromJson(json, AutomationDTO.fromJson).page,
    );
  }

  @override
  Map<String, dynamic> toJson() => _$OutGetAutomationsDTOToJson(this);
}

@JsonSerializable(createJsonKeys: false)
class InPostAutomationDTO implements Json {
  final AutomationDTO automation;

  InPostAutomationDTO({
    required this.automation,
  });

  factory InPostAutomationDTO.fromJson(Map<String, dynamic> json) =>
      _$InPostAutomationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => automation.toJson();
}

@JsonSerializable()
class OutPostAutomationDTO implements Json {
  OutPostAutomationDTO();

  factory OutPostAutomationDTO.fromJson(Map<String, dynamic> json) =>
      _$OutPostAutomationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutPostAutomationDTOToJson(this);
}
