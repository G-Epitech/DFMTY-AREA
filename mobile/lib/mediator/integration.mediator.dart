import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/integration.model.dart';
import 'package:triggo/repositories/integration.repository.dart';

class IntegrationMediator with ChangeNotifier {
  final IntegrationRepository _integrationRepository;

  IntegrationMediator(this._integrationRepository);

  Future<List<Integration>> getIntegrations() async {
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
}
