import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/view/integration_connect.view.dart';

class AutomationCreationSelectIntegrationView extends StatelessWidget {
  final AutomationChoiceEnum type;
  final int indexOfTheTriggerOrAction;

  const AutomationCreationSelectIntegrationView({
    super.key,
    required this.type,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    return IntegrationAvailableView(
      type: type,
      indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
    );
  }
}
