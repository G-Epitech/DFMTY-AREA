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

class InternalServerErrorException extends ApiException {
  InternalServerErrorException([String? message])
      : super(message, "Internal Server Error Exception");
}

class UnauthorizedException extends ApiException {
  UnauthorizedException([String? message])
      : super(message, "Unauthorized Exception");
}

class NotFoundException extends ApiException {
  NotFoundException([String? message]) : super(message, "Not Found Exception");
}

class ForbiddenException extends ApiException {
  ForbiddenException([String? message]) : super(message, "Forbidden Exception");
}

class ConflictException extends ApiException {
  ConflictException([String? message]) : super(message, "Conflict Exception");
}

class UnknownException extends ApiException {
  UnknownException([String? message]) : super(message, "Unknown Exception");
}
