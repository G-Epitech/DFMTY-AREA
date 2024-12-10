import 'package:flutter/material.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationMediator with ChangeNotifier {
  //final AutomationRepository _automationRepository;

  AutomationMediator();

  Future<List<Automation>> getUserAutomations() async {
    // For now, we are returning a hardcoded list of automations
    // since the endpoint isn't done yet
    List<Automation> automations = [
      Automation(
          name: "Reply \"feur\" to \"Quoi\"",
          description: "Last update: Yesterday 20:34",
          iconUri: "assets/icons/bolt.svg",
          iconColor: Colors.orange,
          isActive: true)
    ];
    try {
      /*final res = await _automationRepository.getUserAutomations();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var automation in res.data!.page.data) {
          automations.add(Automation.fromDTO(automation));
        }
        return automations;
      } else {
        throw Exception(res.message);
      }*/
    } catch (e) {
      print("Error: $e");
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
    return automations;
  }
}
