import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/view/integration_connect.view.dart';
import 'package:triggo/mediator/automation.mediator.dart';

class AutomationCreationSelectIntegrationView extends StatelessWidget {
  final AutomationChoiceEnum type;

  const AutomationCreationSelectIntegrationView({
    super.key,
    required this.type,
  });

  @override
  Widget build(BuildContext context) {
    final AutomationMediator automationMediator =
        RepositoryProvider.of<AutomationMediator>(context);
    return IntegrationAvailableView(type: type);
  }
}
