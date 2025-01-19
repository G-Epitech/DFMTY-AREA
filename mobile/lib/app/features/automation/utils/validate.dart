import 'dart:developer';

import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

bool validateTrigger(
    Automation automation, AutomationMediator automationMediator) {
  final schema = automationMediator.automationSchemas;
  final trigger = automation.trigger;

  if (trigger.identifier.isEmpty) {
    log("Trigger identifier is empty");
    return false;
  }

  if (trigger.dependencies.isEmpty) {
    log("Trigger dependencies is empty");
    return false;
  }

  for (final parameter in trigger.parameters) {
    if (parameter.value.isEmpty) {
      log("Trigger parameter value is empty");
      return false;
    }
  }

  final integrationIdentifier = trigger.identifier.split('.').first;
  final triggerOrActionIdentifier = trigger.identifier.split('.').last;

  final integrationSchema = schema!.schemas[integrationIdentifier];
  final triggerSchema = integrationSchema?.triggers[triggerOrActionIdentifier];
  final hasDifferentParameterLength =
      triggerSchema?.parameters.length != trigger.parameters.length;

  if (integrationSchema != null &&
      triggerSchema != null &&
      hasDifferentParameterLength) {
    log("Trigger parameters length is different: ${trigger.parameters.length} - ${triggerSchema.parameters.length}");
    return false;
  }

  return true;
}

bool validateAction(Automation automation,
    AutomationMediator automationMediator, int indexOfTheTriggerOrAction) {
  final schema = automationMediator.automationSchemas;

  if (indexOfTheTriggerOrAction < 0 ||
      indexOfTheTriggerOrAction >= automation.actions.length) {
    log("Index of the trigger or action is out of bounds");
    return false;
  }

  log("Index of the trigger or action: $indexOfTheTriggerOrAction");
  final action = automation.actions[indexOfTheTriggerOrAction];

  if (action.identifier.isEmpty) {
    log("Action identifier is empty");
    return false;
  }

  if (action.dependencies.isEmpty) {
    log("Action dependencies is empty");
    return false;
  }

  for (final parameter in action.parameters) {
    if (parameter.value.isEmpty) {
      log("Action parameter value is empty");
      return false;
    }
  }

  final integrationIdentifier = action.identifier.split('.').first;
  final triggerOrActionIdentifier = action.identifier.split('.').last;

  final integrationSchema = schema!.schemas[integrationIdentifier];
  final actionSchema = integrationSchema?.actions[triggerOrActionIdentifier];
  final hasDifferentParameterLength =
      actionSchema?.parameters.length != action.parameters.length;

  if (integrationSchema != null &&
      actionSchema != null &&
      hasDifferentParameterLength) {
    log("Action parameters length is different: ${action.parameters.length} - ${actionSchema.parameters.length}");
    return false;
  }

  return true;
}

bool validateAutomation(
    Automation automation, AutomationMediator automationMediator) {
  if (!validateTrigger(automation, automationMediator)) {
    return false;
  }

  for (final action in automation.actions) {
    if (!validateAction(
        automation, automationMediator, automation.actions.indexOf(action))) {
      return false;
    }
  }

  return automation.label.isNotEmpty &&
      automation.description.isNotEmpty &&
      automation.trigger.identifier.isNotEmpty &&
      automation.actions.isNotEmpty;
}
