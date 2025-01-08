import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/app/theme/colors/colors.dart';
import 'package:triggo/mediator/integrations/discord.mediator.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration/integration.repository.dart';
import 'package:url_launcher/url_launcher.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;
  final DiscordMediator _discordMediator;

  IntegrationMediator(this._integrationRepository)
      : _discordMediator = DiscordMediator(_integrationRepository.discord);

  get discord => _discordMediator;

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

  Future<void> launchURL(String url) async {
    try {
      final uriURL = Uri.parse(url);
      if (await canLaunchUrl(uriURL)) {
        await launchUrl(uriURL);
      } else {
        throw 'Could not launch $url';
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      rethrow;
    }
  }
}
