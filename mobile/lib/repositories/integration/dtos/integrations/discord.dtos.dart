import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/integration/models/integrations/discord.integrations.dart';
import 'package:triggo/repositories/page/models/page.repository.model.dart';
import 'package:triggo/repositories/page/utils/page_serializer.utils.dart';
import 'package:triggo/utils/json.dart';

part 'discord.dtos.g.dart';

@JsonSerializable()
class InGetUserIntegrationDiscordGuildsDTO implements Json {
  InGetUserIntegrationDiscordGuildsDTO();

  @override
  factory InGetUserIntegrationDiscordGuildsDTO.fromJson(
          Map<String, dynamic> json) =>
      _$InGetUserIntegrationDiscordGuildsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$InGetUserIntegrationDiscordGuildsDTOToJson(this);
}

Page<DiscordGuildDTO> pageIntegrationDiscordGuildsFromJson(
    Map<String, dynamic> json) {
  return pageFromJson<DiscordGuildDTO>(json, DiscordGuildDTO.fromJson);
}

@JsonSerializable()
class OutGetUserIntegrationDiscordGuildsDTO implements Json {
  @JsonKey(fromJson: pageIntegrationDiscordGuildsFromJson, toJson: pageToJson)
  final Page<DiscordGuildDTO> page;

  OutGetUserIntegrationDiscordGuildsDTO({
    required this.page,
  });

  factory OutGetUserIntegrationDiscordGuildsDTO.fromJson(
          Map<String, dynamic> json) =>
      _$OutGetUserIntegrationDiscordGuildsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetUserIntegrationDiscordGuildsDTOToJson(this);
}
