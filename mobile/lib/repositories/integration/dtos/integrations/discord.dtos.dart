import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/repositories/integration/models/integrations/discord.integrations.dart';
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

@JsonSerializable()
class OutGetUserIntegrationDiscordGuildsDTO implements Json {
  final List<DiscordGuildDTO> list;

  OutGetUserIntegrationDiscordGuildsDTO({
    required this.list,
  });

  factory OutGetUserIntegrationDiscordGuildsDTO.fromJson(
          Map<String, dynamic> json) =>
      _$OutGetUserIntegrationDiscordGuildsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetUserIntegrationDiscordGuildsDTOToJson(this);
}

@JsonSerializable()
class InGetUserIntegrationDiscordChannelsDTO implements Json {
  final String guildId;

  InGetUserIntegrationDiscordChannelsDTO({
    required this.guildId,
  });

  factory InGetUserIntegrationDiscordChannelsDTO.fromJson(
          Map<String, dynamic> json) =>
      _$InGetUserIntegrationDiscordChannelsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$InGetUserIntegrationDiscordChannelsDTOToJson(this);
}

@JsonSerializable()
class OutGetUserIntegrationDiscordChannelsDTO implements Json {
  final List<DiscordChannelDTO> list;

  OutGetUserIntegrationDiscordChannelsDTO({
    required this.list,
  });

  factory OutGetUserIntegrationDiscordChannelsDTO.fromJson(
          Map<String, dynamic> json) =>
      _$OutGetUserIntegrationDiscordChannelsDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() =>
      _$OutGetUserIntegrationDiscordChannelsDTOToJson(this);
}
