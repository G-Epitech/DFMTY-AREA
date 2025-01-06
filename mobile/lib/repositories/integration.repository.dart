import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials.repository.dart';
import 'package:triggo/repositories/dtos/integration.dtos.dart';

class IntegrationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  IntegrationRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserIntegrationDTO>> getUserIntegrations(
      {int page = 0, int size = 10}) async {
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
      headers: response.headers,
      data: response.data != null
          ? OutGetUserIntegrationDTO.fromJson({'page': response.data!})
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetUserIntegrationByIdDTO>> getUserIntegrationById(
      String integrationId) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final response = await call(
      method: 'GET',
      endpoint: '/user/integrations/$integrationId',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationByIdDTO>(
      statusCode: response.statusCode,
      message: response.message,
      data: response.data != null
          ? OutGetUserIntegrationByIdDTO.fromJson(
              {'integration': response.data!})
          : null,
      errors: response.errors,
    );
  }

  Future<Response<OutGetIntegrationNamesDTO>> getIntegrationNames() async {
    //    final accessToken = await credentialsRepository.getAccessToken();
    //     final response = await call(
    //       method: 'GET',
    //       endpoint: '/integration',
    //       headers: {'Authorization': 'Bearer $accessToken'},
    //       client: client,
    //     );
    //
    //     return Response<OutGetIntegrationNamesDTO>(
    //       statusCode: response.statusCode,
    //       message: response.message,
    //       data: response.data != null
    //           ? OutGetIntegrationNamesDTO.fromJson(response.data!)
    //           : null,
    //       errors: response.errors,
    //     );
    final json = {
      "page": {
        "pageNumber": 1,
        "pageSize": 10,
        "totalPages": 1,
        "totalRecords": 3,
        "data": [
          {"name": "Discord"},
        ]
      },
    };

    return Response<OutGetIntegrationNamesDTO>(
      statusCode: Codes.ok,
      message: "Ok",
      data: OutGetIntegrationNamesDTO.fromJson(json),
    );
  }

  Future<Response<OutGetIntegrationURIDTO>> getIntegrationURI(
      String name) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'POST',
      endpoint: '/integrations/$name/uri',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetIntegrationURIDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data:
          res.data != null ? OutGetIntegrationURIDTO.fromJson(res.data!) : null,
      errors: res.errors,
    );
  }
}
