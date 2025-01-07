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
          page: pageIntegrationDiscordGuildsFromJson(
              json['page'] as Map<String, dynamic>),
        );

Map<String, dynamic> _$OutGetUserIntegrationDiscordGuildsDTOToJson(
        OutGetUserIntegrationDiscordGuildsDTO instance) =>
    <String, dynamic>{
      'page': pageToJson(instance.page),
    };
