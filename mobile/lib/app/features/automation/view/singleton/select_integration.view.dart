import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/integration/view/integration_connect.view.dart';

class AutomationSelectIntegrationView extends StatelessWidget {
  final AutomationTriggerOrActionType type;
  final int indexOfTheTriggerOrAction;

  const AutomationSelectIntegrationView({
    super.key,
    required this.type,
    required this.indexOfTheTriggerOrAction,
  });

  @override
  Widget build(BuildContext context) {
    context.read<AutomationBloc>().add(
        AutomationResetPending(type: type, index: indexOfTheTriggerOrAction));
    return IntegrationAvailableView(
      type: type,
      indexOfTheTriggerOrAction: indexOfTheTriggerOrAction,
    );
  }
}
