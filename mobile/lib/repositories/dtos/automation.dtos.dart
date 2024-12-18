import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/models/automation.repository.model.dart';
import 'package:triggo/repositories/utils/page_serializer.utils.dart';
import 'package:triggo/utils/json.dart';

import '../models/page.repository.model.dart';

part 'automation.dtos.g.dart';

@JsonSerializable()
class InGetAutomationManifestDTO implements Json {
  InGetAutomationManifestDTO();

  @override
  factory InGetAutomationManifestDTO.fromJson(Map<String, dynamic> json) =>
      _$InGetAutomationManifestDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetAutomationManifestDTOToJson(this);
}

class OutGetAutomationManifestDTO {
  final Map<String, AutomationManifestDTO> manifests;

  OutGetAutomationManifestDTO({
    required this.manifests,
  });

  factory OutGetAutomationManifestDTO.fromJson(Map<String, dynamic> json) {
    return OutGetAutomationManifestDTO(
      manifests: json.map(
        (key, value) => MapEntry(
          key,
          AutomationManifestDTO.fromJson(value as Map<String, dynamic>),
        ),
      ),
    );
  }

  Map<String, dynamic> toJson() {
    return manifests.map(
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

@JsonSerializable()
class OutGetAutomationsDTO implements Json {
  @JsonKey(fromJson: pageAutomationsFromJson, toJson: pageToJson)
  final Page<AutomationDTO> page;

  OutGetAutomationsDTO({
    required this.page,
  });

  factory OutGetAutomationsDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetAutomationsDTOFromJson({'page': json});

  @override
  Map<String, dynamic> toJson() => _$OutGetAutomationsDTOToJson(this);
}

@JsonSerializable()
class InPostAutomationDTO implements Json {
  InPostAutomationDTO();

  factory InPostAutomationDTO.fromJson(Map<String, dynamic> json) =>
      _$InPostAutomationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InPostAutomationDTOToJson(this);
}

@JsonSerializable()
class OutPostAutomationDTO implements Json {
  OutPostAutomationDTO();

  factory OutPostAutomationDTO.fromJson(Map<String, dynamic> json) =>
      _$OutPostAutomationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutPostAutomationDTOToJson(this);
}
