import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/dtos/user.dtos.dart';

class UserRepository {
  final http.Client? client;
  final CredentialsRepository? credentialsRepository;

  UserRepository({required this.client, required this.credentialsRepository});

  Future<Response<OutGetUserDTO>> getUser() async {
    final response = await call(
      method: 'GET',
      endpoint: '/user',
      headers: {
        'Authorization':
            'Bearer ${await credentialsRepository!.getAccessToken()}',
      },
      client: client,
    );

    return Response<OutGetUserDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data:
          response.data != null ? OutGetUserDTO.fromJson(response.data) : null,
      errors: response.errors,
    );
  }
}
