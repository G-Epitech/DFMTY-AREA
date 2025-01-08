import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/integration/models/integration.repository.model.dart';
import 'package:triggo/repositories/page/models/page.repository.model.dart';
import 'package:triggo/repositories/page/utils/page_serializer.utils.dart';
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
class OutGetUserIntegrationDTO implements PageJson<IntegrationDTO> {
  @JsonKey(fromJson: pageIntegrationFromJson, toJson: pageToJson)
  @override
  final Page<IntegrationDTO> page;

  OutGetUserIntegrationDTO({required this.page});

  factory OutGetUserIntegrationDTO.fromJson(Map<String, dynamic> json) {
    return OutGetUserIntegrationDTO(
      page: PageJson.fromJson(json, IntegrationDTO.fromJson).page,
    );
  }

  @override
  Map<String, dynamic> toJson() => _$OutGetUserIntegrationDTOToJson(this);
}

class IntegrationNamesDTO implements Json {
  late final String name;
  late final String iconUri;
  late final String color;
  late final String url;

  IntegrationNamesDTO({
    required this.name,
    required this.iconUri,
    required this.color,
    required this.url,
  });

  @override
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'iconUri': iconUri,
      'color': color,
      'url': url,
    };
  }

  factory IntegrationNamesDTO.fromJson(Map<String, dynamic> json) {
    return IntegrationNamesDTO(
      name: json['name'] as String,
      iconUri: json['iconUri'] as String,
      color: json['color'] as String,
      url: json['url'] as String,
    );
  }
}

Page<IntegrationNamesDTO> pageIntegrationsNameFromJson(
    Map<String, dynamic> json) {
  return pageFromJson<IntegrationNamesDTO>(json, IntegrationNamesDTO.fromJson);
}

@JsonSerializable()
class OutGetIntegrationNamesDTO implements PageJson<IntegrationNamesDTO> {
  @JsonKey(fromJson: pageIntegrationsNameFromJson, toJson: pageToJson)
  @override
  final Page<IntegrationNamesDTO> page;

  OutGetIntegrationNamesDTO({required this.page});

  factory OutGetIntegrationNamesDTO.fromJson(Map<String, dynamic> json) {
    return OutGetIntegrationNamesDTO(
      page: PageJson.fromJson(json, IntegrationNamesDTO.fromJson).page,
    );
  }

  @override
  Map<String, dynamic> toJson() => _$OutGetIntegrationNamesDTOToJson(this);
}

@JsonSerializable()
class OutGetIntegrationURIDTO implements Json {
  final String uri;

  OutGetIntegrationURIDTO({
    required this.uri,
  });

  factory OutGetIntegrationURIDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetIntegrationURIDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetIntegrationURIDTOToJson(this);
}

@JsonSerializable()
class OutGetUserIntegrationByIdDTO implements Json {
  final IntegrationDTO integration;

  OutGetUserIntegrationByIdDTO({
    required this.integration,
  });

  factory OutGetUserIntegrationByIdDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetUserIntegrationByIdDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetUserIntegrationByIdDTOToJson(this);
}
