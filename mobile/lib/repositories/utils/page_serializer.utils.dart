import 'package:triggo/repositories/models/repository.models.dart';

Page<Integration> pageFromJson(Map<String, dynamic> json) {
  return Page<Integration>(
      pageNumber: json['pageNumber'] as int,
      pageSize: json['pageSize'] as int,
      totalPages: json['totalPages'] as int,
      totalRecords: json['totalRecords'] as int,
      data: json['data'] as List<Integration>);
}

Map<String, dynamic> pageToJson(Page<Integration> page) {
  return {
    'pageNumber': page.pageNumber,
    'pageSize': page.pageSize,
    'totalPages': page.totalPages,
    'totalRecords': page.totalRecords,
    'data': page.data,
  };
}
