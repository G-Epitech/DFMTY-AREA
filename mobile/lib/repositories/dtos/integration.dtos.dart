import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/models/repository.models.dart';
import 'package:triggo/repositories/utils/page_serializer.utils.dart';
import 'package:triggo/utils/json.dart';

part 'integration.dtos.g.dart';

@JsonSerializable()
class InGetIntegrationDTO implements Json {
  InGetIntegrationDTO();

  @override
  factory InGetIntegrationDTO.fromJson(Map<String, dynamic> json) =>
      _$InGetIntegrationDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetIntegrationDTOToJson(this);
}

@JsonSerializable()
class OutGetIntegrationDTO implements Json {
  @JsonKey(fromJson: pageFromJson, toJson: pageToJson)
  final Page<Integration> page;

  OutGetIntegrationDTO({
    required this.page,
  });

  factory OutGetIntegrationDTO.fromJson(Map<String, dynamic> json) {
    return _$OutGetIntegrationDTOFromJson(json);
  }

  @override
  Map<String, dynamic> toJson() => _$OutGetIntegrationDTOToJson(this);
}
