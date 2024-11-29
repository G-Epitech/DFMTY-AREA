import 'package:triggo/api/codes.dart';

class Response<T> {
  final Codes statusCode;
  final String message;
  final T? data;
  final List<String>? errors;

  Response({
    required this.statusCode,
    required this.message,
    this.data,
    this.errors,
  });
}
