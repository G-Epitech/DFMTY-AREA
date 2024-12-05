import 'package:json_annotation/json_annotation.dart';
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
  final int pageNumber;
  final int pageSize;
  final int totalPages;
  final int totalRecords;
  final List<dynamic> data;

  OutGetIntegrationDTO({
    required this.pageNumber,
    required this.pageSize,
    required this.totalPages,
    required this.totalRecords,
    required this.data,
  });

  factory OutGetIntegrationDTO.fromJson(Map<String, dynamic> json) {
    return _$OutGetIntegrationDTOFromJson(json);
  }

  @override
  Map<String, dynamic> toJson() => _$OutGetIntegrationDTOToJson(this);
}
