class CallResponse<T> {
  final int statusCode;
  final String message;
  final T? data;

  CallResponse({
    required this.statusCode,
    required this.message,
    this.data,
  });

  factory CallResponse.fromJson(Map<String, dynamic> json) {
    return CallResponse<T>(
      statusCode: 200, // statusCode: json['statusCode'] as int,
      message: 'ok', // message: json['message'] as String,
      data: json as T?, // data: json['data'] as T?,
    );
  }
}
