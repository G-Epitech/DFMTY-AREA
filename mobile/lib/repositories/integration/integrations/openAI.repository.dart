import 'package:http/http.dart' as http;
import 'package:triggo/api/call.dart';
import 'package:triggo/api/response.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/dtos/integrations/openAI.dtos.dart';

class OpenAIRepository {
  final http.Client? client;
  final CredentialsRepository credentialsRepository;

  OpenAIRepository({this.client, required this.credentialsRepository});

  Future<Response<OutOpenAILinkAccountDTO>> linkAccount(
      String apiToken, String adminApiToken) async {
    final accessToken = await credentialsRepository.getAccessToken();
    final res = await call(
      method: 'POST',
      endpoint: '/integrations/openai',
      headers: {'Authorization': 'Bearer $accessToken'},
      client: client,
      body: InLinkOpenAIDTO(apiToken: apiToken, adminApiToken: adminApiToken),
    );

    return Response<OutOpenAILinkAccountDTO>(
      statusCode: res.statusCode,
      message: res.message,
      data:
          res.data != null ? OutOpenAILinkAccountDTO.fromJson(res.data!) : null,
      errors: res.errors,
      headers: res.headers,
    );
  }
}
