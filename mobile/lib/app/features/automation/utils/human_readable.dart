import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/models/automation.model.dart';

String? getHumanReadableValue(
    Automation automation,
    AutomationTriggerOrActionType type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews,
    bool humanReadable) {
  String? value;
  bool isNotHumanReadable = true;
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      if (automation.trigger.identifier !=
          "$integrationIdentifier.$triggerOrActionIdentifier") {
        break;
      }
      for (final parameter in automation.trigger.parameters) {
        if (parameter.identifier == parameterIdentifier) {
          value = parameter.value;
          break;
        }
      }
      break;
    case AutomationTriggerOrActionType.action:
      for (final action in automation.actions) {
        if (action.identifier !=
            "$integrationIdentifier.$triggerOrActionIdentifier") {
          break;
        }
        for (final parameter in action.parameters) {
          if (parameter.identifier == parameterIdentifier) {
            value = parameter.value;
            isNotHumanReadable = parameter.type != 'raw';
            break;
          }
        }
      }
      break;
  }
  if (humanReadable || !isNotHumanReadable) {
    return _replaceByHumanReadable(
        type,
        integrationIdentifier,
        indexOfTheTriggerOrAction,
        triggerOrActionIdentifier,
        parameterIdentifier,
        previews);
  }
  return value;
}

String? _replaceByHumanReadable(
    AutomationTriggerOrActionType type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews) {
  String key = '';
  if (type == AutomationTriggerOrActionType.trigger) {
    key = 'trigger';
  } else {
    key = 'action';
  }
  key += '.$indexOfTheTriggerOrAction';
  key += '.$integrationIdentifier';
  key += '.$triggerOrActionIdentifier';
  key += '.$parameterIdentifier';
  return previews[key];
}
