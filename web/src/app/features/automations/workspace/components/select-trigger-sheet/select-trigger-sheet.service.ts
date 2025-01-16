import { Injectable } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectTriggerSheetState,
  stateUpdaterBack,
  stateUpdaterGoToIntegrationSelection,
  stateUpdaterGoToLinkedIntegrationSelection,
  stateUpdaterSelectIntegration,
  stateUpdaterSelectLinkedIntegration,
} from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.state';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';

@Injectable()
export class SelectTriggerSheetService {
  state = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
  });

  goToIntegrationSelection() {
    patchState(this.state, stateUpdaterGoToIntegrationSelection());
  }

  goToLinkedInegrationSelection() {
    patchState(this.state, stateUpdaterGoToLinkedIntegrationSelection());
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    patchState(this.state, stateUpdaterSelectIntegration(integration));
    this.back();
  }

  selectLinkedIntegration(integration: IntegrationModel): void {
    patchState(this.state, stateUpdaterSelectLinkedIntegration(integration));
    this.back();
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
