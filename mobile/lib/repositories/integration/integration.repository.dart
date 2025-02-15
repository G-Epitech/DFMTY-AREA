import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/authentication/google.repository.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integration.dtos.dart';
import 'package:triggo/repositories/integration/integrations/discord.repository.dart';
import 'package:triggo/repositories/integration/integrations/leagueOfLegends.repository.dart';
import 'package:triggo/repositories/integration/integrations/notion.repository.dart';
import 'package:triggo/repositories/integration/integrations/openAI.repository.dart';

class IntegrationRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;
  final DiscordRepository discordRepository;
  final GoogleRepository googleRepository;
  final NotionRepository notionRepository;
  final OpenAIRepository _openAIRepository;
  final LeagueOfLegendsRepository _leagueOfLegendsRepository;

  IntegrationRepository({this.client, required this.credentialsRepository})
      : discordRepository = DiscordRepository(
          client: client,
          credentialsRepository: credentialsRepository,
        ),
        googleRepository = GoogleRepository(
          client: client,
          credentialsRepository: credentialsRepository,
        ),
        notionRepository = NotionRepository(
          client: client,
          credentialsRepository: credentialsRepository,
        ),
        _openAIRepository = OpenAIRepository(
          client: client,
          credentialsRepository: credentialsRepository,
        ),
        _leagueOfLegendsRepository = LeagueOfLegendsRepository(
          client: client,
          credentialsRepository: credentialsRepository,
        );

  get discord => discordRepository;

  get google => googleRepository;

  get notion => notionRepository;

  get openAI => _openAIRepository;

  get leagueOfLegends => _leagueOfLegendsRepository;

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
          ? OutGetUserIntegrationDTO.fromJson(response.data!)
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
