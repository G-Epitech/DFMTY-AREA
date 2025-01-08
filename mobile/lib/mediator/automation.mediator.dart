import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:triggo/api/codes.dart';
import 'package:triggo/models/automation.model.dart';
import 'package:triggo/repositories/automation/automation.repository.dart';
import 'package:triggo/repositories/automation/dtos/automation.dtos.dart';

class AutomationMediator with ChangeNotifier {
  final AutomationRepository _automationRepository;
  AutomationSchemas? _automationSchemas;

  AutomationMediator(this._automationRepository) {
    _initializeAutomationSchemas();
  }

  AutomationSchemas? get automationSchemas => _automationSchemas;

  Future<void> _initializeAutomationSchemas() async {
    try {
      await _getAutomationSchema();
    } catch (e) {
      log('Error initializing AutomationMediator: $e');
      // Display error message with a snackbar or dialog (something like that)
    }
  }

  Future<List<Automation>> getUserAutomations() async {
    List<Automation> automations = [];
    try {
      final res = await _automationRepository.getUserAutomations();
      if (res.statusCode == Codes.ok && res.data != null) {
        for (var automation in res.data!.page.data) {
          automations.add(Automation.fromDTO(automation));
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

  Future<void> _getAutomationSchema() async {
    final res = await _automationRepository.getAutomationSchema();
    if (res.statusCode == Codes.ok && res.data != null) {
      print('AutomationSchemas: ${res.data!.schema}');
      _automationSchemas = AutomationSchemas.fromDTO(res.data!.schema);
      notifyListeners();
    } else {
      throw Exception(res.message);
    }
  }
}
