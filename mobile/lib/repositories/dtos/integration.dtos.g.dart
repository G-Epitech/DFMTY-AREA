// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'integration.dtos.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

InGetIntegrationDTO _$InGetIntegrationDTOFromJson(Map<String, dynamic> json) =>
    InGetIntegrationDTO();

Map<String, dynamic> _$InGetIntegrationDTOToJson(
        InGetIntegrationDTO instance) =>
    <String, dynamic>{};

OutGetIntegrationDTO _$OutGetIntegrationDTOFromJson(
        Map<String, dynamic> json) =>
    OutGetIntegrationDTO(
      pageNumber: (json['pageNumber'] as num).toInt(),
      pageSize: (json['pageSize'] as num).toInt(),
      totalPages: (json['totalPages'] as num).toInt(),
      totalRecords: (json['totalRecords'] as num).toInt(),
      data: json['data'] as List<dynamic>,
    );

Map<String, dynamic> _$OutGetIntegrationDTOToJson(
        OutGetIntegrationDTO instance) =>
    <String, dynamic>{
      'pageNumber': instance.pageNumber,
      'pageSize': instance.pageSize,
      'totalPages': instance.totalPages,
      'totalRecords': instance.totalRecords,
      'data': instance.data,
    };
