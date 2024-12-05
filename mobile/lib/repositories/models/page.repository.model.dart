class Page<T> {
  final int pageNumber;
  final int pageSize;
  final int totalPages;
  final int totalRecords;
  final List<T> data;

  Page({
    required this.pageNumber,
    required this.pageSize,
    required this.totalPages,
    required this.totalRecords,
    required this.data,
  });
}
