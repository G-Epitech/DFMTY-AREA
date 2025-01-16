import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/models/automation.model.dart';

part 'automation_event.dart';
part 'automation_state.dart';

class AutomationBloc extends Bloc<AutomationEvent, AutomationState> {
  final AutomationMediator _automationMediator;

  AutomationBloc({required AutomationMediator automationMediator})
      : _automationMediator = automationMediator,
        super(AutomationInitial()) {
    on<AutomationSubmitted>(_onSubmitted);
    on<AutomationReset>(_onReset);
    on<AutomationLabelChanged>(_onLabelChanged);
    on<AutomationDescriptionChanged>(_onDescriptionChanged);
    on<AutomationTriggerProviderChanged>(_onTriggerProviderChanged);
    on<AutomationTriggerProviderAdded>(_onTriggerProviderAdded);
    on<AutomationTriggerIdentifierChanged>(_onTriggerIdentifierChanged);
    on<AutomationTriggerParameterChanged>(_onTriggerParameterChanged);
    on<AutomationActionIdentifierChanged>(_onActionIdentifierChanged);
    on<AutomationActionProviderAdded>(_onActionProviderAdded);
    on<AutomationActionParameterChanged>(_onActionParameterChanged);
    on<AutomationActionDeleted>(_onActionDeleted);
    on<AutomationResetPending>(_onResetPending);
    on<AutomationPreviewUpdated>(_onPreviewUpdated);
    on<AutomationValidatePendingAutomation>(_onValidatePendingAutomation);
    on<AutomationLoadCleanAutomation>(_onLoadCleanAutomation);
  }

  Future<void> _onSubmitted(
    AutomationSubmitted event,
    Emitter<AutomationState> emit,
  ) async {
    emit(state.copyWith(status: FormzSubmissionStatus.inProgress));
    try {
      final res =
          await _automationMediator.createAutomation(state.cleanedAutomation);

      if (!res) {
        throw Exception('Failed to create automation');
      }

      emit(state.copyWith(status: FormzSubmissionStatus.success));
    } catch (e) {
      emit(state.copyWith(status: FormzSubmissionStatus.failure));
    }
  }

  void _onLabelChanged(
      AutomationLabelChanged event, Emitter<AutomationState> emit) {
    final updatedAutomation =
        state.cleanedAutomation.copyWith(label: event.label);
    emit(state.copyWith(cleanedAutomation: updatedAutomation));
  }

  void _onDescriptionChanged(
      AutomationDescriptionChanged event, Emitter<AutomationState> emit) {
    final updatedAutomation =
        state.cleanedAutomation.copyWith(description: event.description);
    emit(state.copyWith(cleanedAutomation: updatedAutomation));
  }

  void _onTriggerProviderChanged(
      AutomationTriggerProviderChanged event, Emitter<AutomationState> emit) {
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      identifier: event.triggerName,
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onTriggerProviderAdded(
      AutomationTriggerProviderAdded event, Emitter<AutomationState> emit) {
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      providers: List.from(state.dirtyAutomation.trigger.providers)
        ..add(event.provider),
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onTriggerIdentifierChanged(
      AutomationTriggerIdentifierChanged event, Emitter<AutomationState> emit) {
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      identifier: event.identifier,
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onTriggerParameterChanged(
      AutomationTriggerParameterChanged event, Emitter<AutomationState> emit) {
    bool updated = false;
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
      updatedTrigger.parameters.add(AutomationTriggerParameter(
        identifier: event.parameterIdentifier,
        value: event.parameterValue,
      ));
    }
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);

    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onActionIdentifierChanged(
      AutomationActionIdentifierChanged event, Emitter<AutomationState> emit) {
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
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onActionProviderAdded(
      AutomationActionProviderAdded event, Emitter<AutomationState> emit) {
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

    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onActionParameterChanged(
      AutomationActionParameterChanged event, Emitter<AutomationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
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
    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onResetPending(
      AutomationResetPending event, Emitter<AutomationState> emit) {
    switch (event.type) {
      case AutomationChoiceEnum.trigger:
        final updatedAutomation = state.dirtyAutomation.copyWith(
            trigger: AutomationTrigger(
          identifier: '',
          providers: [],
          parameters: [],
        ));
        emit(state.copyWith(dirtyAutomation: updatedAutomation));
        break;
      case AutomationChoiceEnum.action:
        final updatedActions =
            List<AutomationAction>.from(state.dirtyAutomation.actions);
        if (updatedActions.length > event.index) {
          updatedActions.removeAt(event.index);
        }
        final updatedAutomation =
            state.dirtyAutomation.copyWith(actions: updatedActions);
        emit(state.copyWith(dirtyAutomation: updatedAutomation));
        break;
    }
  }

  void _onActionDeleted(
      AutomationActionDeleted event, Emitter<AutomationState> emit) {
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    updatedActions.removeAt(event.index);
    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(state.copyWith(dirtyAutomation: updatedAutomation));
  }

  void _onReset(AutomationReset event, Emitter<AutomationState> emit) {
    emit(AutomationInitial());
  }

  void _onPreviewUpdated(
      AutomationPreviewUpdated event, Emitter<AutomationState> emit) {
    final updatedPreviews = Map<String, String>.from(state.previews)
      ..[event.key] = event.value;
    final updatedPreviewsSpecialCases =
        _manageSpecialCasesPreviews(updatedPreviews, event.key);
    final updatedAutomation =
        _manageSpecialCasesAutomation(state.dirtyAutomation, event.key);
    emit(state.copyWith(
        dirtyAutomation: updatedAutomation,
        previews: updatedPreviewsSpecialCases));
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

  void _onValidatePendingAutomation(AutomationValidatePendingAutomation event,
      Emitter<AutomationState> emit) {
    emit(state.copyWith(cleanedAutomation: state.dirtyAutomation));
  }

  void _onLoadCleanAutomation(
      AutomationLoadCleanAutomation event, Emitter<AutomationState> emit) {
    emit(state.copyWith(dirtyAutomation: state.cleanedAutomation));
  }
}
