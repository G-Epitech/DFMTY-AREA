import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration.repository.dart';
import 'package:url_launcher/url_launcher.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;

  IntegrationMediator(this._integrationRepository);

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
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<List<String>> getIntegrationNames() async {
    List<String> integrations = [];
    try {
      final res = await _integrationRepository.getIntegrationNames();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var integration in res.data!.page.data) {
          integrations.add(integration.name);
        }
        return integrations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<void> launchURL(String name) async {
    try {
      final res = await _integrationRepository.getIntegrationURI(name);

      if (res.statusCode == Codes.ok && res.data != null) {
        final urlString = res.data!.uri;
        final url = Uri.parse(urlString);
        if (await canLaunchUrl(url)) {
          await launchUrl(url);
        } else {
          throw 'Could not launch $url';
        }
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      print("Error launching URL: $e");
      // Display error message with a snackbar or dialog (something like that)
      rethrow;
    }
  }
}
