import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/dtos/automation.dtos.dart';

class AutomationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  AutomationRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetAutomationManifestDTO>> getAutomationManifest(
      {int page = 0, int size = 10}) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/automations/manifest',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetAutomationManifestDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetAutomationManifestDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }
}
