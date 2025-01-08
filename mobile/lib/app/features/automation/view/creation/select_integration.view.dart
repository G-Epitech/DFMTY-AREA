import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/view/integration_connect.view.dart';

class AutomationCreationSelectIntegrationView extends StatelessWidget {
  final AutomationChoiceEnum type;

  const AutomationCreationSelectIntegrationView({
    super.key,
    required this.type,
  });

  @override
  Widget build(BuildContext context) {
    return IntegrationAvailableView(type: type);
  }
}
