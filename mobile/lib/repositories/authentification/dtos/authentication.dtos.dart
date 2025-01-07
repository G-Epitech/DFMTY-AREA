import 'package:json_annotation/json_annotation.dart';
import 'package:triggo/utils/json.dart';

part 'authentication.dtos.g.dart';

@JsonSerializable()
class InLoginDTO implements Json {
  final String email;
  final String password;

  InLoginDTO({required this.email, required this.password});

  @override
  factory InLoginDTO.fromJson(Map<String, dynamic> json) =>
      _$InLoginDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InLoginDTOToJson(this);
}

@JsonSerializable()
class OutLoginDTO implements Json {
  final String accessToken;
  final String refreshToken;

  OutLoginDTO({required this.accessToken, required this.refreshToken});

  @override
  factory OutLoginDTO.fromJson(Map<String, dynamic> json) =>
      _$OutLoginDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutLoginDTOToJson(this);
}

@JsonSerializable()
class OutRefreshTokenDTO implements Json {
  final String accessToken;
  final String refreshToken;

  OutRefreshTokenDTO({required this.accessToken, required this.refreshToken});

  @override
  factory OutRefreshTokenDTO.fromJson(Map<String, dynamic> json) =>
      _$OutRefreshTokenDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutRefreshTokenDTOToJson(this);
}

@JsonSerializable()
class InRegisterDTO implements Json {
  final String email;
  final String password;
  final String firstName;
  final String lastName;

  InRegisterDTO({
    required this.email,
    required this.password,
    required this.firstName,
    required this.lastName,
  });

  @override
  factory InRegisterDTO.fromJson(Map<String, dynamic> json) =>
      _$InRegisterDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InRegisterDTOToJson(this);
}

@JsonSerializable()
class OutRegisterDTO implements Json {
  final String accessToken;
  final String refreshToken;

  OutRegisterDTO({required this.accessToken, required this.refreshToken});

  @override
  factory OutRegisterDTO.fromJson(Map<String, dynamic> json) =>
      _$OutRegisterDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutRegisterDTOToJson(this);
}
