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

@JsonSerializable()
class OutGetAutomationIDDTO {
  final String id;
  final String label;
  final String description;
  final String ownerId;
  final AutomationTrigger trigger;
  final List<AutomationAction> actions;
  final bool enabled;
  final DateTime updatedAt;

  OutGetAutomationIDDTO({
    required this.id,
    required this.label,
    required this.description,
    required this.ownerId,
    required this.trigger,
    required this.actions,
    required this.enabled,
    required this.updatedAt,
  });

  factory OutGetAutomationIDDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetAutomationIDDTOFromJson(json);

  Map<String, dynamic> toJson() => _$OutGetAutomationIDDTOToJson(this);
}
