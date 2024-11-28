import 'package:triggo/api/codes.dart';

class CallResponse<T> {
  final Codes statusCode;
  final String message;
  final T? data;

  CallResponse({
    required this.statusCode,
    required this.message,
    this.data,
  });
}
