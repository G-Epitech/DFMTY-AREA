import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation/automation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';

void automationUpdateDependencies(
    BuildContext context,
    AutomationTriggerOrActionType type,
    List<String> integrationProviders,
    int indexOfTheTriggerOrAction) {
  switch (type) {
    case AutomationTriggerOrActionType.trigger:
      context.read<AutomationBloc>().add(AutomationTriggerDependenciesUpdated(
          dependencies: integrationProviders));
      break;
    case AutomationTriggerOrActionType.action:
      context.read<AutomationBloc>().add(AutomationActionDependenciesUpdated(
            index: indexOfTheTriggerOrAction,
            dependencies: integrationProviders,
          ));
      break;
  }
}
