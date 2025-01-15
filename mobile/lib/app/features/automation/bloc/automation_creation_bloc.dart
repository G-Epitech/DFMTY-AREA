import 'dart:developer';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

part 'automation_creation_event.dart';
part 'automation_creation_state.dart';

class AutomationCreationBloc
    extends Bloc<AutomationCreationEvent, AutomationCreationState> {
  final AutomationMediator _automationMediator;

  AutomationCreationBloc({required AutomationMediator automationMediator})
      : _automationMediator = automationMediator,
        super(AutomationCreationInitial()) {
    on<AutomationCreationLabelChanged>(_onLabelChanged);
    on<AutomationCreationDescriptionChanged>(_onDescriptionChanged);
    on<AutomationCreationTriggerProviderChanged>(_onTriggerProviderChanged);
    on<AutomationCreationTriggerProviderAdded>(_onTriggerProviderAdded);
    on<AutomationCreationTriggerIdentifierChanged>(_onTriggerIdentifierChanged);
    on<AutomationCreationTriggerParameterChanged>(_onTriggerParameterChanged);
    on<AutomationCreationActionIdentifierChanged>(_onActionIdentifierChanged);
    on<AutomationCreationActionProviderAdded>(_onActionProviderAdded);
    on<AutomationCreationActionParameterChanged>(_onActionParameterChanged);
    on<AutomationCreationActionDeleted>(_onActionDeleted);
    on<AutomationCreationResetPending>(_onResetPending);
    on<AutomationCreationSubmitted>(_onSubmitted);
    on<AutomationCreationReset>(_onReset);
    on<AutomationCreationPreviewUpdated>(_onPreviewUpdated);
    on<AutomationCreationValidatePendingAutomation>(
        _onValidatePendingAutomation);
    on<AutomationCreationLoadCleanAutomation>(_onLoadAutomation);
  }

  void _onLabelChanged(AutomationCreationLabelChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation =
        state.cleanedAutomation.copyWith(label: event.label);
    emit(AutomationCreationDirty(
        updatedAutomation, updatedAutomation, state.previews, state.status));
  }

  void _onDescriptionChanged(AutomationCreationDescriptionChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedAutomation =
        state.cleanedAutomation.copyWith(description: event.description);
    emit(AutomationCreationDirty(
        updatedAutomation, updatedAutomation, state.previews, state.status));
  }

  void _onTriggerProviderChanged(AutomationCreationTriggerProviderChanged event,
      Emitter<AutomationCreationState> emit) {
    log("Trigger name changed: ${event.triggerName}");
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      identifier: event.triggerName,
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onTriggerProviderAdded(AutomationCreationTriggerProviderAdded event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      providers: List.from(state.dirtyAutomation.trigger.providers)
        ..add(event.provider),
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    log('Trigger provider added: ${updatedAutomation.trigger.providers.length}');
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onTriggerIdentifierChanged(
      AutomationCreationTriggerIdentifierChanged event,
      Emitter<AutomationCreationState> emit) {
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      identifier: event.identifier,
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    log('Trigger identifier changed: ${updatedAutomation.trigger.identifier}');
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onTriggerParameterChanged(
      AutomationCreationTriggerParameterChanged event,
      Emitter<AutomationCreationState> emit) {
    bool updated = false;
    log('!===== dirtyAutomation Trigger parameter base: ${state.dirtyAutomation.trigger.parameters.length} =====');
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      parameters: state.dirtyAutomation.trigger.parameters.map((param) {
        if (param.identifier == event.parameterIdentifier) {
          updated = true;
          return param.copyWith(value: event.parameterValue);
        }
        return param;
      }).toList(),
    );
    if (!updated) {
      log('!===== Parameter not found, Should add it =====');
      updatedTrigger.parameters.add(AutomationTriggerParameter(
        identifier: event.parameterIdentifier,
        value: event.parameterValue,
      ));
    }
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);

    log('!===== Trigger parameter changed: ${updatedAutomation.trigger.parameters.length} =====');
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onActionIdentifierChanged(
      AutomationCreationActionIdentifierChanged event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    if (updatedActions.length <= event.index) {
      updatedActions.add(AutomationAction(
        identifier: event.identifier,
        providers: [],
        parameters: [],
      ));
    } else {
      updatedActions[event.index] = updatedActions[event.index].copyWith(
        identifier: event.identifier,
      );
    }

    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onActionProviderAdded(AutomationCreationActionProviderAdded event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    if (updatedActions.length <= event.index) {
      updatedActions.add(AutomationAction(
        identifier: '',
        providers: [event.provider],
        parameters: [],
      ));
    } else {
      updatedActions[event.index] = updatedActions[event.index].copyWith(
        providers: List.from(updatedActions[event.index].providers)
          ..add(event.provider),
      );
    }
    log('Action provider added: ${updatedActions[event.index].providers.length}');

    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onActionParameterChanged(AutomationCreationActionParameterChanged event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    log('/!\\ Action parameter changed: ${event.parameterIdentifier}');
    log('/!\\ Action parameter changed to value: ${event.parameterValue}');
    bool updated = false;
    if (event.index < updatedActions.length) {
      updatedActions[event.index] = updatedActions[event.index].copyWith(
        parameters: updatedActions[event.index].parameters.map((param) {
          if (param.identifier == event.parameterIdentifier) {
            updated = true;
            return param.copyWith(
              value: event.parameterValue,
              type: event.parameterType,
            );
          }
          return param;
        }).toList(),
      );
      if (!updated) {
        updatedActions[event.index].parameters.add(AutomationActionParameter(
              identifier: event.parameterIdentifier,
              value: event.parameterValue,
              type: event.parameterType,
            ));
      }
    } else {
      updatedActions.add(AutomationAction(
        identifier: '',
        providers: [],
        parameters: [
          AutomationActionParameter(
            identifier: event.parameterIdentifier,
            value: event.parameterValue,
            type: event.parameterType,
          ),
        ],
      ));
    }
    log('Action length: ${updatedActions.length}');
    log('Action index: ${event.index}');
    log('Action parameter changed: ${updatedActions[event.index].parameters.length}');
    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onResetPending(AutomationCreationResetPending event,
      Emitter<AutomationCreationState> emit) {
    switch (event.type) {
      case AutomationChoiceEnum.trigger:
        log('Resetting trigger');
        final updatedAutomation = state.dirtyAutomation.copyWith(
            trigger: AutomationTrigger(
          identifier: '',
          providers: [],
          parameters: [],
        ));
        emit(AutomationCreationState(state.cleanedAutomation, updatedAutomation,
            state.previews, state.status));
        break;
      case AutomationChoiceEnum.action:
        final updatedActions =
            List<AutomationAction>.from(state.dirtyAutomation.actions);
        if (updatedActions.length > event.index) {
          updatedActions.removeAt(event.index);
        }
        final updatedAutomation =
            state.dirtyAutomation.copyWith(actions: updatedActions);
        emit(AutomationCreationState(state.cleanedAutomation, updatedAutomation,
            state.previews, state.status));
        break;
    }
  }

  void _onSubmitted(AutomationCreationSubmitted event,
      Emitter<AutomationCreationState> emit) async {
    emit(AutomationCreationState(state.cleanedAutomation, state.dirtyAutomation,
        state.previews, FormzSubmissionStatus.inProgress));
    try {
      final res =
          await _automationMediator.createAutomation(state.cleanedAutomation);

      print('Response Code: ${res}');

      if (!res) {
        throw Exception('Failed to create automation');
      }

      emit(AutomationCreationState(
          state.cleanedAutomation,
          state.dirtyAutomation,
          state.previews,
          FormzSubmissionStatus.success));
    } catch (e) {
      emit(AutomationCreationState(
          state.cleanedAutomation,
          state.dirtyAutomation,
          state.previews,
          FormzSubmissionStatus.failure));
    }
  }

  void _onActionDeleted(AutomationCreationActionDeleted event,
      Emitter<AutomationCreationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    updatedActions.removeAt(event.index);
    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(AutomationCreationDirty(state.cleanedAutomation, updatedAutomation,
        state.previews, state.status));
  }

  void _onReset(
      AutomationCreationReset event, Emitter<AutomationCreationState> emit) {
    emit(AutomationCreationInitial());
  }

  void _onPreviewUpdated(AutomationCreationPreviewUpdated event,
      Emitter<AutomationCreationState> emit) {
    final updatedPreviews = Map<String, String>.from(state.previews)
      ..[event.key] = event.value;
    final updatedPreviewsSpecialCases =
        _manageSpecialCasesPreviews(updatedPreviews, event.key);
    final updatedAutomation =
        _manageSpecialCasesAutomation(state.dirtyAutomation, event.key);
    log('Preview key: ${event.key}');
    emit(AutomationCreationState(state.cleanedAutomation, updatedAutomation,
        updatedPreviewsSpecialCases, state.status));
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

  void _onValidatePendingAutomation(
      AutomationCreationValidatePendingAutomation event,
      Emitter<AutomationCreationState> emit) {
    log('Validate: ${state.dirtyAutomation.trigger.providers.length}');
    emit(AutomationCreationState(state.dirtyAutomation, state.dirtyAutomation,
        state.previews, state.status));
  }

  void _onLoadAutomation(AutomationCreationLoadCleanAutomation event,
      Emitter<AutomationCreationState> emit) {
    emit(AutomationCreationState(state.cleanedAutomation,
        state.cleanedAutomation, state.previews, state.status));
  }
}
