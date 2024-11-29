abstract class Json {
  Map<String, dynamic> toJson();
  factory Json.fromJson(Map<String, dynamic> json) =>
      throw UnimplementedError();
}
