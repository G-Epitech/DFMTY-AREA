import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/discord.dtos.dart';

class DiscordRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  DiscordRepository({this.client, required this.credentialsRepository});

  Future<Response<OutGetUserIntegrationDiscordGuildsDTO>> getGuilds(
      String id) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'POST',
      endpoint: '/integrations/$id/discord/guilds',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
    );

    return Response<OutGetUserIntegrationDiscordGuildsDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data: res.data != null
          ? OutGetUserIntegrationDiscordGuildsDTO.fromJson(res.data!)
          : null,
      errors: res.errors,
    );
  }
}
