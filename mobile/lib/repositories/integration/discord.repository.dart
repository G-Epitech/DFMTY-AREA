import 'package:http/http.dart' as http;
import 'package:triggo/api/codes.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/discord.dtos.dart';
import 'package:triggo/repositories/integration/models/integrations/discord.integrations.dart';
import 'package:triggo/repositories/page/models/page.repository.model.dart';

class DiscordRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  DiscordRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserIntegrationDiscordGuildsDTO>> getGuilds(
      String id) async {
/*    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'POST',
      endpoint: '/integrations/$id/discord/guilds',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );*/

    return Response<OutGetUserIntegrationDiscordGuildsDTO>(
      statusCode: Codes.ok,
      message: "OK",
      data: OutGetUserIntegrationDiscordGuildsDTO(
        page: Page(
            pageNumber: 0,
            pageSize: 10,
            totalPages: 4,
            totalRecords: 20,
            data: [
              DiscordGuildDTO(
                id: '1',
                name: 'Guild 1',
                iconUri: 'https://cdn.discordapp.com/icons/1/1.png',
                approximateMemberCount: 100,
                linked: true,
              ),
              DiscordGuildDTO(
                id: '2',
                name: 'Guild 2',
                iconUri: 'https://cdn.discordapp.com/icons/2/2.png',
                approximateMemberCount: 200,
                linked: false,
              ),
              DiscordGuildDTO(
                id: '3',
                name: 'Guild 3',
                iconUri: 'https://cdn.discordapp.com/icons/3/3.png',
                approximateMemberCount: 300,
                linked: true,
              ),
              DiscordGuildDTO(
                id: '4',
                name: 'Guild 4',
                iconUri: 'https://cdn.discordapp.com/icons/4/4.png',
                approximateMemberCount: 400,
                linked: false,
              ),
              DiscordGuildDTO(
                id: '5',
                name: 'Guild 5',
                iconUri: 'https://cdn.discordapp.com/icons/5/5.png',
                approximateMemberCount: 500,
                linked: true,
              ),
            ]),
      ),
      errors: null,
    );
  }
}
