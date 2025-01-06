import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/automation.model.dart';
import 'package:triggo/repositories/automation.repository.dart';
import 'package:triggo/repositories/dtos/automation.dtos.dart';

class AutomationMediator with ChangeNotifier {
  final AutomationRepository _automationRepository;

  AutomationMediator(this._automationRepository);

  Future<List<Automation>> getUserAutomations() async {
    List<Automation> automations = [];
    try {
      final res = await _automationRepository.getUserAutomations();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var automation in res.data!.page.data) {
          automations.add(Automation(
              name: automation.label,
              description: automation.description,
              iconUri: "assets/icons/chat.svg",
              iconColor: Colors.orange,
              isActive: automation.enabled));
        }
        return automations;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      return [];
    }
  }

  Future<bool> createAutomation() async {
    try {
      InPostAutomationDTO automation = InPostAutomationDTO();
      final res = await _automationRepository.createAutomation(automation);
      if (res.statusCode == Codes.created) {
        return true;
      } else {
        throw Exception(res.message);
      }
    } catch (e) {
      // Display error message with a snackbar or dialog (something like that)
      return false;
    }
  }
}
