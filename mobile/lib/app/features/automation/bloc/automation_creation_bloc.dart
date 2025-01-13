import 'dart:developer';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/models/automation.model.dart';

part 'automation_creation_event.dart';
part 'automation_creation_state.dart';

class AutomationCreationBloc
    extends Bloc<AutomationCreationEvent, AutomationCreationState> {
  AutomationCreationBloc() : super(AutomationCreationInitial()) {
    on<AutomationCreationLabelChanged>(_onLabelChanged);
    on<AutomationCreationDescriptionChanged>(_onDescriptionChanged);
    on<AutomationCreationTriggerProviderChanged>(_onTriggerProviderChanged);
    on<AutomationCreationTriggerProviderAdded>(_onTriggerProviderAdded);
    on<AutomationCreationTriggerIdentifierChanged>(_onTriggerIdentifierChanged);
    on<AutomationCreationTriggerParameterChanged>(_onTriggerParameterChanged);
    on<AutomationCreationActionProviderAdded>(_onActionProviderAdded);
    on<AutomationCreationActionParameterChanged>(_onActionParameterChanged);
    on<AutomationCreationActionDeleted>(_onActionDeleted);
    on<AutomationCreationResetPending>(_onResetPending);
    on<AutomationCreationSubmitted>(_onSubmitted);
    on<AutomationCreationReset>(_onReset);
    on<AutomationCreationPreviewUpdated>(_onPreviewUpdated);
  }

  void _onLabelChanged(AutomationCreationLabelChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation = state.automation.copyWith(label: event.label);
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onDescriptionChanged(AutomationCreationDescriptionChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation =
        state.automation.copyWith(description: event.description);
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onTriggerProviderChanged(AutomationCreationTriggerProviderChanged event,
      Emitter<AutomationCreationState> emit) {
    log("Trigger name changed: ${event.triggerName}");
    final updatedTrigger = state.automation.trigger.copyWith(
      identifier: event.triggerName,
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onTriggerProviderAdded(AutomationCreationTriggerProviderAdded event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.automation.trigger.copyWith(
      providers: List.from(state.automation.trigger.providers)
        ..add(event.provider),
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    log('Trigger provider added: ${updatedAutomation.trigger.providers.length}');
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onTriggerIdentifierChanged(
      AutomationCreationTriggerIdentifierChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.automation.trigger.copyWith(
      identifier: event.identifier,
    );
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    log('Trigger identifier changed: ${updatedAutomation.trigger.identifier}');
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onTriggerParameterChanged(
      AutomationCreationTriggerParameterChanged event,
      Emitter<AutomationCreationState> emit) {
    bool updated = false;
    final updatedTrigger = state.automation.trigger.copyWith(
      parameters: state.automation.trigger.parameters.map((param) {
        if (param.identifier == event.parameterIdentifier) {
          updated = true;
          return param.copyWith(value: event.parameterValue);
        }
        return param;
      }).toList(),
    );
    if (!updated) {
      updatedTrigger.parameters.add(AutomationTriggerParameter(
        identifier: event.parameterIdentifier,
        value: event.parameterValue,
      ));
    }
    final updatedAutomation =
        state.automation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
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
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
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
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onResetPending(AutomationCreationResetPending event,
      Emitter<AutomationCreationState> emit) {
    switch (event.type) {
      case AutomationChoiceEnum.trigger:
        log('Resetting trigger');
        final updatedAutomation = state.automation.copyWith(
            trigger: AutomationTrigger(
          identifier: '',
          providers: [],
          parameters: [],
        ));
        emit(AutomationCreationState(updatedAutomation, state.previews, false));
        break;
      case AutomationChoiceEnum.action:
        final updatedActions =
            List<AutomationAction>.from(state.automation.actions);
        if (updatedActions.length > event.index) {
          updatedActions.removeAt(event.index);
        }
        final updatedAutomation =
            state.automation.copyWith(actions: updatedActions);
        emit(AutomationCreationState(updatedAutomation, state.previews, false));
        break;
    }
  }

  void _onSubmitted(AutomationCreationSubmitted event,
      Emitter<AutomationCreationState> emit) {
    if (state.isValid) {
      log('Automation submitted');
    } else {
      log('Invalid automation');
      emit(AutomationCreationState(state.automation, state.previews, false));
    }
  }

  void _onActionDeleted(AutomationCreationActionDeleted event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.automation.actions);
    updatedActions.removeAt(event.index);
    final updatedAutomation =
        state.automation.copyWith(actions: updatedActions);
    emit(AutomationCreationState(
        updatedAutomation, state.previews, _isValid(updatedAutomation)));
  }

  void _onReset(
      AutomationCreationReset event, Emitter<AutomationCreationState> emit) {
    emit(AutomationCreationInitial());
  }

  bool _isValid(Automation automation) {
    return automation.label.isNotEmpty &&
        automation.trigger.identifier.isNotEmpty;
  }

  void _onPreviewUpdated(AutomationCreationPreviewUpdated event,
      Emitter<AutomationCreationState> emit) {
    final updatedPreviews = Map<String, String>.from(state.previews)
      ..[event.key] = event.value;
    final updatedPreviewsSpecialCases =
        _manageSpecialCasesPreviews(updatedPreviews, event.key);
    final updatedAutomation =
        _manageSpecialCasesAutomation(state.automation, event.key);
    emit(AutomationCreationState(
        updatedAutomation, updatedPreviewsSpecialCases, state.isValid));
  }

  Map<String, String> _manageSpecialCasesPreviews(
      Map<String, String> newPreviews, String key) {
    final previews = Map<String, String>.from(newPreviews);
    if (key == 'trigger.0.discord.MessageReceivedInChannel.GuildId') {
      previews.remove('trigger.0.discord.MessageReceivedInChannel.ChannelId');
    }
    return previews;
  }

  Automation _manageSpecialCasesAutomation(Automation automation, String key) {
    final updatedAutomation = automation;
    if (key == 'trigger.0.discord.MessageReceivedInChannel.GuildId') {
      updatedAutomation.trigger.parameters
          .removeWhere((param) => param.identifier == 'ChannelId');
    }
    return updatedAutomation;
  }
}
