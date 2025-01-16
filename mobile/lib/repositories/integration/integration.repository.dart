import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/authentication/google.repository.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/discord.repository.dart';
import 'package:triggo/repositories/integration/dtos/integration.dtos.dart';
import 'package:triggo/repositories/integration/leagueOfLegends.repository.dart';
import 'package:triggo/repositories/integration/notion.repository.dart';
import 'package:triggo/repositories/integration/openAI.repository.dart';

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
      "pageNumber": 1,
      "pageSize": 10,
      "totalPages": 1,
      "totalRecords": 3,
      "data": [
        {
          "name": "Discord",
          "iconUri": 'assets/icons/discord.svg',
          "color": "#7289da",
          "url": "discord"
        },
        {
          "name": "Notion",
          "iconUri": 'assets/icons/notion.svg',
          "color": "#000000",
          "url": "notion"
        },
        {
          "name": "OpenAI",
          "iconUri": 'assets/icons/openai.svg',
          "color": "#10a37f",
          "url": "openAI"
        },
        {
          "name": "LeagueOfLegends",
          "iconUri": 'assets/icons/league_of_legends.svg',
          "color": "#c89b3c",
          "url": "leagueOfLegends"
        }
      ]
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
