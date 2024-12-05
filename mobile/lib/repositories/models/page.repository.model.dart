import 'package:triggo/repositories/models/repository.models.dart';

class Page {
  final int pageNumber;
  final int pageSize;
  final int totalPages;
  final int totalRecords;
  final List<Integration> data;

  Page({
    required this.pageNumber,
    required this.pageSize,
    required this.totalPages,
    required this.totalRecords,
    required this.data,
  });
}
