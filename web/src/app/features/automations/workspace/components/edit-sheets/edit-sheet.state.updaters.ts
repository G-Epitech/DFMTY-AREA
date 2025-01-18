import { PartialStateUpdater } from '@ngrx/signals';

import { EditSheetStateInterface } from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.interface';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import { AutomationStepSelectionType } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';

export function stateUpdaterBack(): PartialStateUpdater<EditSheetStateInterface> {
  return (state: EditSheetStateInterface) => {
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
): PartialStateUpdater<EditSheetStateInterface> {
  return (state: EditSheetStateInterface) => {
    return {
      ...state,
      selectedIntegration: integration,
    };
  };
}

export function stateUpdaterSelectLinkedIntegration(
  integration: IntegrationModel
): PartialStateUpdater<EditSheetStateInterface> {
  return (state: EditSheetStateInterface) => {
    return {
      ...state,
      selectedLinkedIntegration: integration,
    };
  };
}

export function stateUpdaterGoTo(
  step: AutomationStepSelectionType
): PartialStateUpdater<EditSheetStateInterface> {
  return (state: EditSheetStateInterface) => {
    return {
      ...state,
      selectionStep: step,
      stepHistory: [...state.stepHistory, step],
    };
  };
}
