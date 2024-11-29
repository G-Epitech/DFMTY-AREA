abstract class ToJson {
  Map<String, dynamic> toJson();
}

abstract class FromJson {
  factory FromJson.fromJson(Map<String, dynamic> json) =>
      throw UnimplementedError();
}
