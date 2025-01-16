import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/notion.dtos.dart';

class NotionRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  NotionRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserIntegrationNotionDatabasesDTO>> getDatabases(
      String id) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'GET',
      endpoint: '/integrations/$id/notion/databases',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationNotionDatabasesDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data: res.data != null
          ? OutGetUserIntegrationNotionDatabasesDTO.fromJson({'list': res.data})
          : null,
      errors: null,
    );
  }

  Future<Response<OutGetUserIntegrationNotionPagesDTO>> getPages(
      String id) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'GET',
      endpoint: '/integrations/$id/notion/pages',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationNotionPagesDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data: res.data != null
          ? OutGetUserIntegrationNotionPagesDTO.fromJson({'list': res.data})
          : null,
      errors: null,
    );
  }
}
