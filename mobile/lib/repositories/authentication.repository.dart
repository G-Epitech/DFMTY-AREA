import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/dtos/authentication.dtos.dart';

class AuthenticationRepository {
  Future<Response<OutLoginDTO>> login(String email, String password) async {
    return call<InLoginDTO, OutLoginDTO>(
      method: 'POST',
      endpoint: '/auth/login',
      body: InLoginDTO(email: email, password: password),
    );
  }
}
