class CallResponse<T> {
  final int statusCode;
  final String message;
  final T? data;

  CallResponse({
    required this.statusCode,
    required this.message,
    this.data,
  });
}
