class ApiException implements Exception {
  final String? _prefix, _message;

  ApiException([this._message, this._prefix]);

  @override
  String toString() {
    return "$_prefix: $_message";
  }
}

class InternetException extends ApiException {
  InternetException([String? message]) : super(message, "no Internet");
}

class ServerTimeOutException extends ApiException {
  ServerTimeOutException([String? message]) : super(message, "server Timeout");
}

class BadRequestException extends ApiException {
  BadRequestException([String? message])
      : super(message, "Bad Request Exception");
}

class InvalidInputException extends ApiException {
  InvalidInputException([String? message])
      : super(message, "Invalid input Exception Exception");
}

class UnauthorizedException extends ApiException {
  UnauthorizedException([String? message])
      : super(message, "Unauthorized Exception");
}
