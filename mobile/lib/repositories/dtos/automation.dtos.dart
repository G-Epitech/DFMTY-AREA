import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/models/automation.repository.model.dart';
import 'package:triggo/utils/json.dart';

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
  final Map<String, AutomationManifest> manifests;

  OutGetAutomationManifestDTO({
    required this.manifests,
  });

  factory OutGetAutomationManifestDTO.fromJson(Map<String, dynamic> json) {
    return OutGetAutomationManifestDTO(
      manifests: json.map(
        (key, value) => MapEntry(
          key,
          AutomationManifest.fromJson(value as Map<String, dynamic>),
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
