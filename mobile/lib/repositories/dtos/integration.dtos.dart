import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/models/repository.models.dart';
import 'package:triggo/repositories/utils/page_serializer.utils.dart';
import 'package:triggo/utils/json.dart';

part 'integration.dtos.g.dart';

@JsonSerializable()
class InGetUserIntegrationDTO implements Json {
  InGetUserIntegrationDTO();

  @override
  factory InGetUserIntegrationDTO.fromJson(Map<String, dynamic> json) =>
      _$InGetUserIntegrationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetUserIntegrationDTOToJson(this);
}

Page<IntegrationDTO> pageIntegrationFromJson(Map<String, dynamic> json) {
  return pageFromJson<IntegrationDTO>(json, IntegrationDTO.fromJson);
}

@JsonSerializable()
class OutGetUserIntegrationDTO implements Json {
  @JsonKey(fromJson: pageIntegrationFromJson, toJson: pageToJson)
  final Page<IntegrationDTO> page;

  OutGetUserIntegrationDTO({
    required this.page,
  });

  factory OutGetUserIntegrationDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetUserIntegrationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetUserIntegrationDTOToJson(this);
}

Page<String> pageIntegrationsNameFromJson(Map<String, dynamic> json) {
  return pageFromJson<String>(json, (e) => e as String);
}

@JsonSerializable()
class OutGetIntegrationDTO implements Json {
  @JsonKey(fromJson: pageIntegrationsNameFromJson, toJson: pageToJson)
  final Page<String> page;

  OutGetIntegrationDTO({
    required this.page,
  });

  factory OutGetIntegrationDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetIntegrationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetIntegrationDTOToJson(this);
}
