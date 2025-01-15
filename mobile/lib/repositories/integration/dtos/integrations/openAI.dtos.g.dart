// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'openAI.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InLinkOpenAIDTO _$InLinkOpenAIDTOFromJson(Map<String, dynamic> json) =>
    InLinkOpenAIDTO(
      apiToken: json['apiToken'] as String,
      adminApiToken: json['adminApiToken'] as String,
    );

Map<String, dynamic> _$InLinkOpenAIDTOToJson(InLinkOpenAIDTO instance) =>
    <String, dynamic>{
      'apiToken': instance.apiToken,
      'adminApiToken': instance.adminApiToken,
    };
