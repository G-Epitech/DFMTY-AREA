import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/dtos/integration.dtos.dart';

class IntegrationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  IntegrationRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetIntegrationDTO>> getIntegrations() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetIntegrationDTO>> getIntegrationByPage(int page) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/?page=$page',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetIntegrationDTO>> getIntegrationByPageAndSize(
      int page, int size) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/?page=$page&size=$size',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }
}
