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
  final String token;
  final String refreshToken;

  OutLoginDTO({required this.token, required this.refreshToken});

  @override
  factory OutLoginDTO.fromJson(Map<String, dynamic> json) =>
      _$OutLoginDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutLoginDTOToJson(this);
}

@JsonSerializable()
class InRefreshTokenDTO implements Json {
  final String refreshToken;

  InRefreshTokenDTO({required this.refreshToken});

  @override
  factory InRefreshTokenDTO.fromJson(Map<String, dynamic> json) =>
      _$InRefreshTokenDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InRefreshTokenDTOToJson(this);
}

@JsonSerializable()
class OutRefreshTokenDTO implements Json {
  final String token;

  OutRefreshTokenDTO({required this.token});

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
  final String name;

  InRegisterDTO({
    required this.email,
    required this.password,
    required this.name,
  });

  @override
  factory InRegisterDTO.fromJson(Map<String, dynamic> json) =>
      _$InRegisterDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$InRegisterDTOToJson(this);
}

@JsonSerializable()
class OutRegisterDTO implements Json {
  final String token;
  final String refreshToken;

  OutRegisterDTO({required this.token, required this.refreshToken});

  @override
  factory OutRegisterDTO.fromJson(Map<String, dynamic> json) =>
      _$OutRegisterDTOFromJson(json);

  @override
  Map<String, dynamic> toJson() => _$OutRegisterDTOToJson(this);
}
