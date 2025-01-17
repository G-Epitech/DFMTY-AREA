import { AvailableIntegrationType } from '@common/types';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { PartialStateUpdater } from '@ngrx/signals';
import { IntegrationModel } from '@models/integration';
import { AutomationSchemaTrigger } from '@models/automation';

export interface SelectTriggerSheetState {
  selectionStep: TriggerSelectionStep;
  stepHistory: TriggerSelectionStep[];
  selectedIntegration: AvailableIntegrationType | null;
  selectedLinkedIntegration: IntegrationModel | null;
  selectedTrigger: AutomationSchemaTrigger | null;
}

export function stateUpdaterBack(): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    const history = state.stepHistory;
    if (history.length <= 1) {
      return state;
    }
    history.pop();
    return {
      ...state,
      selectionStep: history[history.length - 1],
      stepHistory: history,
    };
  };
}

export function stateUpdaterSelectIntegration(
  integration: AvailableIntegrationType
): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectedIntegration: integration,
    };
  };
}

export function stateUpdaterSelectLinkedIntegration(
  integration: IntegrationModel
): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectedLinkedIntegration: integration,
    };
  };
}

export function stateUpdaterSelectTrigger(
  trigger: AutomationSchemaTrigger
): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectedTrigger: trigger,
    };
  };
}

export function stateUpdaterGoTo(
  step: TriggerSelectionStep
): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectionStep: step,
      stepHistory: [...state.stepHistory, step],
    };
  };
}
