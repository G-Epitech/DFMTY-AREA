enum Codes {
  ok(200),
  created(201),
  noContent(204),
  badRequest(400),
  unauthorized(401),
  forbidden(403),
  notFound(404),
  conflict(409),
  internalServerError(500);

  final int code;
  const Codes(this.code);

  static Codes fromStatusCode(int statusCode) {
    return Codes.values.firstWhere(
      (e) => e.code == statusCode,
      orElse: () => Codes.internalServerError,
    );
  }
}
