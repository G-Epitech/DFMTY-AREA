import 'package:triggo/repositories/page/models/page.repository.model.dart';
import 'package:triggo/utils/json.dart';

Page<T> pageFromJson<T>(
    Map<String, dynamic> json, T Function(Map<String, dynamic>) fromJson) {
  return Page(
    pageNumber: json['pageNumber'],
    pageSize: json['pageSize'],
    totalPages: json['totalPages'],
    totalRecords: json['totalRecords'],
    data: (json['data'] as List)
        .map((e) => fromJson(e as Map<String, dynamic>))
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

class PageJson<T> implements Json {
  final Page<T> page;

  PageJson({required this.page});

  @override
  Map<String, dynamic> toJson() {
    return {
      'page': pageToJson(page),
    };
  }

  factory PageJson.fromJson(
      Map<String, dynamic> json, T Function(Map<String, dynamic>) fromJson) {
    return PageJson(
      page: pageFromJson(json, fromJson),
    );
  }
}
