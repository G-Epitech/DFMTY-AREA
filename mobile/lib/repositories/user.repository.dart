import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/models/user.model.dart';
import 'package:triggo/repositories/dtos/user.dtos.dart';

class UserRepository {
  final http.Client? client;

  UserRepository({this.client});

  Future<User?> getUser() async {
    final response = await call(
      method: 'GET',
      endpoint: '/user',
      client: client,
    );

    if (response.data != null) {
      final outGetUserDTO = OutGetUserDTO.fromJson(response.data);
      final user = User(
        email: outGetUserDTO.email,
        firstName: outGetUserDTO.firstName,
        lastName: outGetUserDTO.lastName,
        picture: outGetUserDTO.picture,
      );
      return user;
    }
    return null;
  }
}
