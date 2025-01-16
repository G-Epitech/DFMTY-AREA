import { AvailableIntegrationType } from '@common/types';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { PartialStateUpdater } from '@ngrx/signals';

export interface SelectTriggerSheetState {
  selectionStep: TriggerSelectionStep;
  stepHistory: TriggerSelectionStep[];
  selectedIntegration: AvailableIntegrationType | null;
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

export function stateUpdaterGoToIntegrationSelection(): PartialStateUpdater<SelectTriggerSheetState> {
  return (state: SelectTriggerSheetState) => {
    return {
      ...state,
      selectionStep: TriggerSelectionStep.INTEGRATION,
      stepHistory: [...state.stepHistory, TriggerSelectionStep.INTEGRATION],
    };
  };
}
