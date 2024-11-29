class InLoginDTO {
  final String email;
  final String password;

  InLoginDTO({required this.email, required this.password});

  Map<String, dynamic> toJson() {
    return {
      'email': email,
      'password': password,
    };
  }
}

class OutLoginDTO {
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
