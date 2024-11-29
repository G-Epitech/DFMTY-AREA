import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/dtos/authentication.dtos.dart';

class AuthenticationRepository {
  final http.Client? client;

  AuthenticationRepository({this.client});

  Future<Response<OutLoginDTO>> login(String email, String password) async {
    final response = await call<InLoginDTO>(
      method: 'POST',
      endpoint: '/auth/login',
      body: InLoginDTO(email: email, password: password),
      client: client,
    );

    return Response<OutLoginDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null ? OutLoginDTO.fromJson(response.data) : null,
      errors: response.errors,
    );
  }

  Future<Response<OutRefreshTokenDTO>> refreshToken(String refreshToken) async {
    final response = await call<InRefreshTokenDTO>(
      method: 'POST',
      endpoint: '/auth/refresh',
      body: InRefreshTokenDTO(refreshToken: refreshToken),
      client: client,
    );

    return Response<OutRefreshTokenDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutRefreshTokenDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutRegisterDTO>> register(
      String email, String password, String name) async {
    final response = await call<InRegisterDTO>(
      method: 'POST',
      endpoint: '/auth/register',
      body: InRegisterDTO(email: email, password: password, name: name),
      client: client,
    );

    return Response<OutRegisterDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data:
          response.data != null ? OutRegisterDTO.fromJson(response.data) : null,
      errors: response.errors,
    );
  }
}
