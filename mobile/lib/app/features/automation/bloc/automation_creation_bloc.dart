import 'dart:developer';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:triggo/models/automation.model.dart';

part 'automation_creation_event.dart';
part 'automation_creation_state.dart';

class AutomationCreationBloc
    extends Bloc<AutomationCreationEvent, AutomationCreationState> {
  AutomationCreationBloc() : super(AutomationCreationInitial()) {
    on<AutomationCreationLabelChanged>(_onLabelChanged);
    on<AutomationCreationDescriptionChanged>(_onDescriptionChanged);
    on<AutomationCreationTriggerNameChanged>(_onTriggerNameChanged);
    on<AutomationCreationTriggerProviderAdded>(_onTriggerProviderAdded);
    on<AutomationCreationTriggerParameterChanged>(_onTriggerParameterChanged);
    on<AutomationCreationActionProviderAdded>(_onActionProviderAdded);
    on<AutomationCreationActionParameterChanged>(_onActionParameterChanged);
    on<AutomationCreationSubmitted>(_onSubmitted);
    on<AutomationCreationReset>(_onReset);
  }

  void _onLabelChanged(AutomationCreationLabelChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation = state.automation.copyWith(label: event.label);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onDescriptionChanged(AutomationCreationDescriptionChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation =
        state.automation.copyWith(description: event.description);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onTriggerNameChanged(AutomationCreationTriggerNameChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.automation.trigger.copyWith(
      identifier: event.triggerName,
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onTriggerProviderAdded(AutomationCreationTriggerProviderAdded event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.automation.trigger.copyWith(
      providers: List.from(state.automation.trigger.providers)
        ..add(event.provider),
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onTriggerParameterChanged(
      AutomationCreationTriggerParameterChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.automation.trigger.copyWith(
      parameters: state.automation.trigger.parameters.map((param) {
        if (param.identifier == event.parameterIdentifier) {
          return param.copyWith(value: event.parameterValue);
        }
        return param;
      }).toList(),
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onActionProviderAdded(AutomationCreationActionProviderAdded event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.automation.actions);
    updatedActions[event.index] = updatedActions[event.index].copyWith(
      providers: List.from(updatedActions[event.index].providers)
        ..add(event.provider),
    );
    final updatedAutomation =
        state.automation.copyWith(actions: updatedActions);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onActionParameterChanged(AutomationCreationActionParameterChanged event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.automation.actions);
    updatedActions[event.index] = updatedActions[event.index].copyWith(
      parameters: updatedActions[event.index].parameters.map((param) {
        if (param.identifier == event.parameterIdentifier) {
          return param.copyWith(
            value: event.parameterValue,
            type: event.parameterType,
          );
        }
        return param;
      }).toList(),
    );
    final updatedAutomation =
        state.automation.copyWith(actions: updatedActions);
    emit(AutomationCreationState(
        updatedAutomation, _isValid(updatedAutomation)));
  }

  void _onSubmitted(AutomationCreationSubmitted event,
      Emitter<AutomationCreationState> emit) {
    if (state.isValid) {
      log('Automation submitted');
    } else {
      log('Invalid automation');
      emit(AutomationCreationState(state.automation, false));
    }
  }

  void _onReset(
      AutomationCreationReset event, Emitter<AutomationCreationState> emit) {
    emit(AutomationCreationInitial());
  }

  bool _isValid(Automation automation) {
    return automation.label.isNotEmpty &&
        automation.trigger.identifier.isNotEmpty;
  }
}
