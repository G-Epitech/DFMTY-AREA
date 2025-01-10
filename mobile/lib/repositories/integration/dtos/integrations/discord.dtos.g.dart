// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'discord.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetUserIntegrationDiscordGuildsDTO
    _$InGetUserIntegrationDiscordGuildsDTOFromJson(Map<String, dynamic> json) =>
        InGetUserIntegrationDiscordGuildsDTO();

Map<String, dynamic> _$InGetUserIntegrationDiscordGuildsDTOToJson(
        InGetUserIntegrationDiscordGuildsDTO instance) =>
    <String, dynamic>{};

OutGetUserIntegrationDiscordGuildsDTO
    _$OutGetUserIntegrationDiscordGuildsDTOFromJson(
            Map<String, dynamic> json) =>
        OutGetUserIntegrationDiscordGuildsDTO(
          list: (json['list'] as List<dynamic>)
              .map((e) => DiscordGuildDTO.fromJson(e as Map<String, dynamic>))
              .toList(),
        );

Map<String, dynamic> _$OutGetUserIntegrationDiscordGuildsDTOToJson(
        OutGetUserIntegrationDiscordGuildsDTO instance) =>
    <String, dynamic>{
      'list': instance.list,
    };

InGetUserIntegrationDiscordChannelsDTO
    _$InGetUserIntegrationDiscordChannelsDTOFromJson(
            Map<String, dynamic> json) =>
        InGetUserIntegrationDiscordChannelsDTO(
          guildId: json['guildId'] as String,
        );

Map<String, dynamic> _$InGetUserIntegrationDiscordChannelsDTOToJson(
        InGetUserIntegrationDiscordChannelsDTO instance) =>
    <String, dynamic>{
      'guildId': instance.guildId,
    };

OutGetUserIntegrationDiscordChannelsDTO
    _$OutGetUserIntegrationDiscordChannelsDTOFromJson(
            Map<String, dynamic> json) =>
        OutGetUserIntegrationDiscordChannelsDTO(
          list: (json['list'] as List<dynamic>)
              .map((e) => DiscordChannelDTO.fromJson(e as Map<String, dynamic>))
              .toList(),
        );

Map<String, dynamic> _$OutGetUserIntegrationDiscordChannelsDTOToJson(
        OutGetUserIntegrationDiscordChannelsDTO instance) =>
    <String, dynamic>{
      'list': instance.list,
    };
