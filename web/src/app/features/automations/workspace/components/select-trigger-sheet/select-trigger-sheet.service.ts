import { Injectable } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectTriggerSheetState,
  stateUpdaterBack,
  stateUpdaterGoToIntegrationSelection,
  stateUpdaterSelectIntegration,
} from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.state';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { AvailableIntegrationType } from '@common/types';

@Injectable()
export class SelectTriggerSheetService {
  state = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
  });

  goToIntegrationSelection() {
    patchState(this.state, stateUpdaterGoToIntegrationSelection());
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    patchState(this.state, stateUpdaterSelectIntegration(integration));
    this.back();
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
