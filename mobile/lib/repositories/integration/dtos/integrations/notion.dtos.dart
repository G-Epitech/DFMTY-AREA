import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/integration/models/integrations/notion.integrations.dart';
import 'package:triggo/utils/json.dart';

part 'notion.dtos.g.dart';

@JsonSerializable()
class InGetUserIntegrationNotionDatabasesDTO implements Json {
  InGetUserIntegrationNotionDatabasesDTO();

  @override
  factory InGetUserIntegrationNotionDatabasesDTO.fromJson(
          Map<String, dynamic> json) =>
      _$InGetUserIntegrationNotionDatabasesDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$InGetUserIntegrationNotionDatabasesDTOToJson(this);
}

@JsonSerializable()
class OutGetUserIntegrationNotionDatabasesDTO implements Json {
  final List<NotionDatabaseDTO> list;

  OutGetUserIntegrationNotionDatabasesDTO({
    required this.list,
  });

  factory OutGetUserIntegrationNotionDatabasesDTO.fromJson(
          Map<String, dynamic> json) =>
      _$OutGetUserIntegrationNotionDatabasesDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetUserIntegrationNotionDatabasesDTOToJson(this);
}

@JsonSerializable()
class InGetUserIntegrationNotionPagesDTO implements Json {
  final String guildId;

  InGetUserIntegrationNotionPagesDTO({
    required this.guildId,
  });

  factory InGetUserIntegrationNotionPagesDTO.fromJson(
          Map<String, dynamic> json) =>
      _$InGetUserIntegrationNotionPagesDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$InGetUserIntegrationNotionPagesDTOToJson(this);
}

@JsonSerializable()
class OutGetUserIntegrationNotionPagesDTO implements Json {
  final List<NotionPageDTO> list;

  OutGetUserIntegrationNotionPagesDTO({
    required this.list,
  });

  factory OutGetUserIntegrationNotionPagesDTO.fromJson(
          Map<String, dynamic> json) =>
      _$OutGetUserIntegrationNotionPagesDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetUserIntegrationNotionPagesDTOToJson(this);
}
