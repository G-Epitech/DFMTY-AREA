import 'package:triggo/api/utils/dto.abstract.dart';

class InLoginDTO implements ToJson {
  final String email;
  final String password;

  InLoginDTO({required this.email, required this.password});

  @override
  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'password': password,
    };
  }
}

class OutLoginDTO implements FromJson {
  final String token;
  final String refreshToken;

  OutLoginDTO({required this.token, required this.refreshToken});

  factory OutLoginDTO.fromJson(Map<String, dynamic> json) {
    return OutLoginDTO(
      token: json['token'],
      refreshToken: json['refreshToken'],
    );
  }
}

class InRefreshTokenDTO implements ToJson {
  final String refreshToken;

  InRefreshTokenDTO({required this.refreshToken});

  @override
  Map<String, dynamic> toJson() {
    return {
      'refreshToken': refreshToken,
    };
  }
}

class OutRefreshTokenDTO implements FromJson {
  final String token;

  OutRefreshTokenDTO({required this.token});

  factory OutRefreshTokenDTO.fromJson(Map<String, dynamic> json) {
    return OutRefreshTokenDTO(
      token: json['token'],
    );
  }
}

class InRegisterDTO implements ToJson {
  final String email;
  final String password;
  final String name;

  InRegisterDTO(
      {required this.email, required this.password, required this.name});

  @override
  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'password': password,
      'name': name,
    };
  }
}

class OutRegisterDTO implements FromJson {
  final String token;
  final String refreshToken;

  OutRegisterDTO({required this.token, required this.refreshToken});

  factory OutRegisterDTO.fromJson(Map<String, dynamic> json) {
    return OutRegisterDTO(
      token: json['token'],
      refreshToken: json['refreshToken'],
    );
  }
}
