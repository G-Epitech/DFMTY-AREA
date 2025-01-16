import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/leagueOfLegends.dtos.dart';

class LeagueOfLegendsRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  LeagueOfLegendsRepository({this.client, required this.credentialsRepository});

  Future<Response<OutLeagueOfLegendsLinkAccountDTO>> linkAccount(
      String gameName, String tagLine) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'POST',
      endpoint: '/integrations/lol',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
      body: InLinkLeagueOfLegendsDTO(gameName: gameName, tagLine: tagLine),
    );

    return Response<OutLeagueOfLegendsLinkAccountDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data: res.data != null
          ? OutLeagueOfLegendsLinkAccountDTO.fromJson(res.data!)
          : null,
      errors: res.errors,
      headers: res.headers,
    );
  }
}
