import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/automation/dtos/automation.dtos.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';

class AutomationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  AutomationRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetAutomationSchemaDTO>> getAutomationSchema(
      {int page = 0, int size = 10}) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/automations/schema',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetAutomationSchemaDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetAutomationSchemaDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetAutomationIDDTO>> getAutomationById(String id) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/automations/$id',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetAutomationIDDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetAutomationIDDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetAutomationsDTO>> getUserAutomations() async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/automations',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetAutomationsDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutGetAutomationsDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutPostAutomationDTO>> createAutomation(
      InPostAutomationDTO inPostAutomationDTO) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call<InPostAutomationDTO>(
      method: 'POST',
      endpoint: '/automations',
      body: inPostAutomationDTO,
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutPostAutomationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutPostAutomationDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutDeleteAutomationDTO>> deleteAutomation(String id) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'DELETE',
      endpoint: '/automations/$id',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );
    return Response<OutDeleteAutomationDTO>(
      statusCode: response.statusCode,
      message: response.message,
      headers: response.headers,
      data: response.data != null
          ? OutDeleteAutomationDTO.fromJson(response.data)
          : null,
      errors: response.errors,
    );
  }
}
