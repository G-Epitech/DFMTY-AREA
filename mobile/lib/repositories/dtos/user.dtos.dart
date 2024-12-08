import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/utils/json.dart';

part 'user.dtos.g.dart';

@JsonSerializable()
class InGetUserDTO implements Json {
  InGetUserDTO();

  @override
  factory InGetUserDTO.fromJson(Map<String, dynamic> json) =>
      _$InGetUserDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InGetUserDTOToJson(this);
}

@JsonSerializable()
class OutGetUserDTO implements Json {
  final String id;
  final String firstName;
  final String lastName;
  final String email;
  final String picture;

  OutGetUserDTO({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.email,
    required this.picture,
  });

  @override
  factory OutGetUserDTO.fromJson(Map<String, dynamic> json) =>
      _$OutGetUserDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutGetUserDTOToJson(this);
}
