import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:triggo/app/features/automation/bloc/automation_creation_bloc.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';

void automationUpdateProvider(BuildContext context, AutomationChoiceEnum type,
    String integrationProvider, int indexOfTheTriggerOrAction) {
  switch (type) {
    case AutomationChoiceEnum.trigger:
      context
          .read<AutomationCreationBloc>()
          .add(AutomationCreationTriggerProviderAdded(
            provider: integrationProvider,
          ));
      break;
    case AutomationChoiceEnum.action:
      context
          .read<AutomationCreationBloc>()
          .add(AutomationCreationActionProviderAdded(
            index: indexOfTheTriggerOrAction,
            provider: integrationProvider,
          ));
      break;
  }
}
