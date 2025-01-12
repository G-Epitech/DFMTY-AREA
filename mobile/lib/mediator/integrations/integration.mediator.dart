import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/mediator/integrations/discord.mediator.dart';
import 'package:triggo/mediator/integrations/google.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/credentials/credentials.repository.dart';
import 'package:triggo/repositories/integration/integration.repository.dart';
import 'package:triggo/utils/launch_url.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;
  final DiscordMediator _discordMediator;
  final GoogleMediator _googleMediator;
  final CredentialsRepository _credentialsRepository;

  IntegrationMediator(this._integrationRepository, this._credentialsRepository)
      : _discordMediator = DiscordMediator(_integrationRepository.discord),
        _googleMediator = GoogleMediator(
            _integrationRepository.google, _credentialsRepository);

  get discord => _discordMediator;
  get google => _googleMediator;

  Future<List<Integration>> getUserIntegrations() async {
    List<Integration> integrations = [];
    try {
      final res = await _integrationRepository.getUserIntegrations();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.page.data) {
          integrations.add(Integration.fromDTO(integration));
        }
        return integrations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<List<AvailableIntegration>> getIntegrationNames() async {
    List<AvailableIntegration> integrations = [];
    try {
      final res = await _integrationRepository.getIntegrationNames();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.page.data) {
          integrations.add(AvailableIntegration(
            name: integration.name,
            iconUri: integration.iconUri,
            color: HexColor(integration.color),
            url: integration.url,
          ));
        }
        return integrations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<void> launchURLFromIntegration(String name) async {
    try {
      final res = await _integrationRepository.getIntegrationURI(name);

      if (res.statusCode == Codes.ok && res.data != null) {
        await launchURL(res.data!.uri);
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      rethrow;
    }
  }
}
