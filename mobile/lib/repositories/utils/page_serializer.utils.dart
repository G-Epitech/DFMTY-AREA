import 'package:triggo/repositories/models/repository.models.dart';

Page pageFromJson(Map<String, dynamic> json) {
  return Page(
    pageNumber: json['pageNumber'],
    pageSize: json['pageSize'],
    totalPages: json['totalPages'],
    totalRecords: json['totalRecords'],
    data: (json['data'] as List<Integration>)
        .map((e) => Integration.fromJson(e as Map<String, dynamic>))
        .toList(),
  );
}

Map<String, dynamic> pageToJson(Page page) {
  return {
    'pageNumber': page.pageNumber,
    'pageSize': page.pageSize,
    'totalPages': page.totalPages,
    'totalRecords': page.totalRecords,
    'data': page.data.map((e) => e.toJson()).toList(),
  };
}
