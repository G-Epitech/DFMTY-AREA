import 'package:triggo/app/features/automation/models/input.model.dart';

AutomationInputType getParameterType(String type) {
  switch (type) {
    case 'String':
      return AutomationInputType.text;
    case 'Float':
      return AutomationInputType.number;
    case 'Integer':
      return AutomationInputType.number;
    case 'Boolean':
      return AutomationInputType.boolean;
    case 'Datetime':
      return AutomationInputType.date;
    default:
      return AutomationInputType.text;
  }
}
