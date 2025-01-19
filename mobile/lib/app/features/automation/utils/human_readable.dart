import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/models/automation.model.dart';

AutomationParameterModel? getHumanReadableValue(
    Automation automation,
    AutomationTriggerOrActionType type,
    String integrationIdentifier,
    int indexOfTheTriggerOrAction,
    String triggerOrActionIdentifier,
    String parameterIdentifier,
    Map<String, String> previews) {
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      if (automation.trigger.identifier !=
          "$integrationIdentifier.$triggerOrActionIdentifier") {
        break;
      }
      for (final param in automation.trigger.parameters) {
        if (param.identifier == parameterIdentifier) {
          return AutomationParameterModel(value: param.value, type: 'raw');
        }
      }
      break;
    case AutomationTriggerOrActionType.action:
      for (final action in automation.actions) {
        if (action.identifier !=
            "$integrationIdentifier.$triggerOrActionIdentifier") {
          break;
        }
        for (final param in action.parameters) {
          if (param.identifier == parameterIdentifier) {
            return AutomationParameterModel(
                value: param.value, type: param.type);
          }
        }
      }
      break;
  }
  return null;
}

String? replaceByHumanReadable(
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
