import 'package:triggo/app/features/automation/models/input.model.dart';

AutomationInputType getInputType(AutomationParameterType type) {
  switch (type) {
    case AutomationParameterType.restrictedRadio:
      return AutomationInputType.radio;
    case AutomationParameterType.restrictedRadioBlocked:
      return AutomationInputType.radio;
    case AutomationParameterType.number:
      return AutomationInputType.number;
    case AutomationParameterType.emoji:
      return AutomationInputType.emoji;
    default:
      return AutomationInputType.text;
  }
}
