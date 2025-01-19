import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/models/input.model.dart';
import 'package:triggo/app/features/automation/utils/input_type.dart';
import 'package:triggo/app/features/automation/view/singleton/input.view.dart';
import 'package:triggo/app/features/automation/widgets/input_parameter.dart';
import 'package:triggo/app/features/automation/widgets/parameter_from_other.dart';
import 'package:triggo/app/routes/routes_names.dart';
import 'package:triggo/app/widgets/scaffold.triggo.dart';
import 'package:triggo/models/automation.model.dart';

class AutomationParameterChoice extends StatelessWidget {
  final String title;
  final void Function(String, String, String, String) onSave;
  final AutomationTriggerOrActionType type;
  final Automation automation;
  final String? value;
  final int indexOfTheTriggerOrAction;
  final AutomationSchemaTriggerActionProperty property;
  final AutomationParameterType parameterType;
  final String? previousValueType;

  const AutomationParameterChoice({
    super.key,
    required this.title,
    required this.onSave,
    required this.type,
    required this.automation,
    required this.value,
    required this.indexOfTheTriggerOrAction,
    required this.property,
    required this.parameterType,
    required this.previousValueType,
  });

  @override
  Widget build(BuildContext context) {
    return BaseScaffold(
      title: title,
      getBack: true,
      body: ListView(
        children: [
          _buildFromPreviousTriggerAction(context),
          const SizedBox(height: 8.0),
          _buildManualInput(context),
        ],
      ),
    );
  }

  Widget _buildFromPreviousTriggerAction(BuildContext context) {
    return AutomationInputParameterWithLabel(
      title: "From a previous trigger/action",
      previewData:
          "Select a value that resulted from a previous trigger/action",
      input: AutomationParameterFromOther(
        type: type,
        automation: automation,
        label: title,
        onSave: onSave,
        value: value,
        indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
        property: property,
      ),
    );
  }

  Widget _buildManualInput(BuildContext context) {
    return AutomationInputParameterWithLabel(
      title: "Manual input",
      previewData: "Enter manually a value",
      input: AutomationInputView(
        type: getInputType(parameterType),
        label: title,
        routeToGoWhenSave: RoutesNames.popTwoTimes,
        onSave: (value, humanReadableValue) {
          onSave(value, 'raw', humanReadableValue, "any");
        },
        value: previousValueType?.toLowerCase() == 'raw' ? value : null,
      ),
    );
  }
}
