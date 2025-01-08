import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/user/dtos/user.dtos.dart';

class UserRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  UserRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserDTO>> getUser() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user',
      headers: {'Authorization': 'Bearer $accessToken'},
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
