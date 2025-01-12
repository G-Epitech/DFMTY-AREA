import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/google.dtos.dart';

class GoogleRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  GoogleRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetGoogleOAuth2Configuration>>
      getGoogleOAuth2Configuration() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'GET',
      endpoint: '/auth/oauth2/google/configuration',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );
    return Response<OutGetGoogleOAuth2Configuration>(
      statusCode: res.statusCode,
      message: res.message,
      data: res.data != null
          ? OutGetGoogleOAuth2Configuration.fromJson(res.data)
          : null,
      errors: null,
    );
  }

  Future<Response<OutGetGoogleOAuth2Credentials>> getGoogleOAuth2Credentials(
      String accessToken, String refreshToken, String tokenType) async {
    final res = await call(
      method: 'POST',
      endpoint: '/auth/oauth2/google/from-credentials',
      headers: {'Authorization': 'Bearer $accessToken'},
      body: InGetGoogleOAuth2Credentials(
        refreshToken: refreshToken,
        tokenType: tokenType,
      ),
      client: client,
    );

    return Response<OutGetGoogleOAuth2Credentials>(
      statusCode: res.statusCode,
      message: res.message,
      headers: res.headers,
      data: res.data != null
          ? OutGetGoogleOAuth2Credentials.fromJson(res.data)
          : null,
      errors: null,
    );
  }
}
