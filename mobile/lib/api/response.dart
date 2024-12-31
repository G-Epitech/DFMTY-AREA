import 'package:triggo/api/codes.dart';

class Response<T> {
  final Codes statusCode;
  final String message;
  final T? data;
  final Map<String, List<String>>? errors;
  final Map<String, String>? headers;

  Response({
    required this.statusCode,
    required this.message,
    this.data,
    this.errors,
    this.headers,
  });
}

class ProblemDetails {
  final String? type;
  final String? title;
  final int? status;
  final String? detail;
  final String? instance;
  final Map<String, List<String>>? errors;

  ProblemDetails({
    this.type,
    this.title,
    this.status,
    this.detail,
    this.instance,
    this.errors,
  });
}
