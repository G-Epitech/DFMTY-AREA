import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:formz/formz.dart';
import 'package:triggo/app/features/automation/models/choice.model.dart';
import 'package:triggo/app/features/automation/view/singleton/parameters.view.dart';
import 'package:triggo/mediator/automation.mediator.dart';
import 'package:triggo/mediator/integration.mediator.dart';
import 'package:triggo/models/automation.model.dart';

part 'automation_event.dart';
part 'automation_state.dart';

class AutomationBloc extends Bloc<AutomationEvent, AutomationState> {
  final AutomationMediator _automationMediator;
  final IntegrationMediator _integrationMediator;

  AutomationBloc(
      {required AutomationMediator automationMediator,
      required IntegrationMediator integrationMediator})
      : _automationMediator = automationMediator,
        _integrationMediator = integrationMediator,
        super(AutomationInitial()) {
    on<AutomationSubmitted>(_onSubmitted);
    on<AutomationReset>(_onReset);
    on<AutomationLabelChanged>(_onLabelChanged);
    on<AutomationDescriptionChanged>(_onDescriptionChanged);
    on<AutomationTriggerDependenciesUpdated>(_onTriggerDependenciesUpdated);
    on<AutomationTriggerIdentifierChanged>(_onTriggerIdentifierChanged);
    on<AutomationTriggerParameterChanged>(_onTriggerParameterChanged);
    on<AutomationActionIdentifierChanged>(_onActionIdentifierChanged);
    on<AutomationActionDependenciesUpdated>(_onActionDependenciesUpdated);
    on<AutomationActionParameterChanged>(_onActionParameterChanged);
    on<AutomationActionDeleted>(_onActionDeleted);
    on<AutomationResetPending>(_onResetPending);
    on<AutomationPreviewUpdated>(_onPreviewUpdated);
    on<AutomationLoadDirtyToClean>(_onValidatePendingAutomation);
    on<AutomationLoadCleanToDirty>(_onLoadCleanAutomation);
    on<AutomationLoadExisting>(_onLoadExistingAutomation);
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

  void _onTriggerDependenciesUpdated(AutomationTriggerDependenciesUpdated event,
      Emitter<AutomationState> emit) {
    final previews = _getCleanPreviews();
    final updatedTrigger = state.dirtyAutomation.trigger.copyWith(
      dependencies: event.dependencies,
    );
    final updatedAutomation =
        state.dirtyAutomation.copyWith(trigger: updatedTrigger);
    emit(state.copyWith(
      dirtyAutomation: updatedAutomation,
      previews: previews,
    ));
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
        dependencies: [],
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

  void _onActionDependenciesUpdated(AutomationActionDependenciesUpdated event,
      Emitter<AutomationState> emit) {
    final previews = _getCleanPreviews();
    final List<AutomationAction> updatedActions =
        List.from(state.dirtyAutomation.actions);
    if (updatedActions.length <= event.index) {
      updatedActions.add(AutomationAction(
        identifier: '',
        dependencies: event.dependencies,
        parameters: [],
      ));
    } else {
      updatedActions[event.index] = updatedActions[event.index].copyWith(
        dependencies: event.dependencies,
      );
    }

    final updatedAutomation =
        state.dirtyAutomation.copyWith(actions: updatedActions);
    emit(state.copyWith(
      dirtyAutomation: updatedAutomation,
      previews: previews,
    ));
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
        dependencies: [],
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
          dependencies: [],
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

  void _onValidatePendingAutomation(
      AutomationLoadDirtyToClean event, Emitter<AutomationState> emit) {
    emit(state.copyWith(cleanedAutomation: state.dirtyAutomation));
  }

  void _onLoadCleanAutomation(
      AutomationLoadCleanToDirty event, Emitter<AutomationState> emit) {
    final newPreview = _getCleanPreviews();
    emit(state.copyWith(
        dirtyAutomation: state.cleanedAutomation, previews: newPreview));
  }

  Map<String, String> _getCleanPreviews() {
    final Map<String, String> newPreview = {};

    for (final trigger in [state.cleanedAutomation.trigger]) {
      for (final param in trigger.parameters) {
        final index = [state.cleanedAutomation.trigger].indexOf(trigger);
        final integrationIdentifier = trigger.identifier.split('.').first;
        final triggerIdentifier = trigger.identifier.split('.').last;
        final previewKey =
            "trigger.$index.$integrationIdentifier.$triggerIdentifier.${param.identifier}";
        newPreview[previewKey] = state.previews[previewKey] ?? '';
      }
    }

    for (final action in state.cleanedAutomation.actions) {
      for (final param in action.parameters) {
        final index = state.cleanedAutomation.actions.indexOf(action);
        final integrationIdentifier = action.identifier.split('.').first;
        final actionIdentifier = action.identifier.split('.').last;
        final previewKey =
            "action.$index.$integrationIdentifier.$actionIdentifier.${param.identifier}";
        newPreview[previewKey] = state.previews[previewKey] ?? '';
      }
    }

    return newPreview;
  }

  Future<void> _onLoadExistingAutomation(
      AutomationLoadExisting event, Emitter<AutomationState> emit) async {
    final previews = await _previewsFromUnknownAutomation(event.automation);

    emit(state.copyWith(
      cleanedAutomation: event.automation,
      dirtyAutomation: event.automation,
      previews: previews,
      status: FormzSubmissionStatus.initial,
    ));
  }

  Future<Map<String, String>> _previewsFromUnknownAutomation(
      Automation automation) async {
    final Map<String, String> newPreview = {};

    for (final trigger in [automation.trigger]) {
      for (final param in trigger.parameters) {
        final index = [automation.trigger].indexOf(trigger);
        final integrationIdentifier = trigger.identifier.split('.').first;
        final triggerIdentifier = trigger.identifier.split('.').last;
        final previewKey =
            "trigger.$index.$integrationIdentifier.$triggerIdentifier.${param.identifier}";
        newPreview[previewKey] = param.value;

        final options = await getOptionsFromMediator(
            automation,
            AutomationChoiceEnum.trigger,
            integrationIdentifier,
            index,
            triggerIdentifier,
            param.identifier,
            _integrationMediator);

        for (final option in options) {
          if (option.value == param.value) {
            newPreview[previewKey] = option.title;
            break;
          }
        }
      }
    }

    for (final action in automation.actions) {
      for (final param in action.parameters) {
        final index = automation.actions.indexOf(action);
        final integrationIdentifier = action.identifier.split('.').first;
        final actionIdentifier = action.identifier.split('.').last;
        final previewKey =
            "action.$index.$integrationIdentifier.$actionIdentifier.${param.identifier}";
        newPreview[previewKey] = param.value;

        final options = await getOptionsFromMediator(
            automation,
            AutomationChoiceEnum.action,
            integrationIdentifier,
            index,
            actionIdentifier,
            param.identifier,
            _integrationMediator);

        for (final option in options) {
          if (option.value == param.value) {
            newPreview[previewKey] = option.title;
            break;
          }
        }
      }
    }

    return newPreview;
  }
}
