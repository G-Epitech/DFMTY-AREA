import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/dtos/integration.dtos.dart';

class IntegrationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  IntegrationRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserIntegrationDTO>> getUserIntegrations() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetUserIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetUserIntegrationDTO>> getUserIntegrationByPage(
      int page) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/?page=$page',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetUserIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetUserIntegrationDTO>> getUserIntegrationByPageAndSize(
      int page, int size) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/?page=$page&size=$size',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetUserIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetUserIntegrationDTO>> getUserIntegrationById(
      String integrationId) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/$integrationId',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetUserIntegrationDTO.fromJson(response.data!)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetIntegrationDTO>> getIntegration() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/integrations',
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
