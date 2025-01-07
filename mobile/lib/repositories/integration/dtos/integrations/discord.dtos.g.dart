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
