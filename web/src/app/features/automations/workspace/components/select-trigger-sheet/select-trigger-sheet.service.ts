import { Injectable } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectTriggerSheetState,
  stateUpdaterBack,
  stateUpdaterGoTo,
  stateUpdaterSelectIntegration,
  stateUpdaterSelectLinkedIntegration,
  stateUpdaterSelectTrigger,
} from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.state';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import { AutomationSchemaTrigger } from '@models/automation';

@Injectable()
export class SelectTriggerSheetService {
  state = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
    selectedTrigger: null,
  });

  goToIntegrationSelection() {
    patchState(this.state, stateUpdaterGoTo(TriggerSelectionStep.INTEGRATION));
  }

  goToLinkedInegrationSelection() {
    patchState(
      this.state,
      stateUpdaterGoTo(TriggerSelectionStep.LINKED_INTEGRATION)
    );
  }

  goToTriggerSelection() {
    patchState(this.state, stateUpdaterGoTo(TriggerSelectionStep.TRIGGER));
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    patchState(this.state, stateUpdaterSelectIntegration(integration));
    patchState(this.state, state => ({
      ...state,
      selectedLinkedIntegration: null,
      selectedTrigger: null,
    }));
    this.back();
  }

  selectLinkedIntegration(integration: IntegrationModel): void {
    patchState(this.state, stateUpdaterSelectLinkedIntegration(integration));
    patchState(this.state, state => ({
      ...state,
      selectedTrigger: null,
    }));
    this.back();
  }

  selectTrigger(trigger: AutomationSchemaTrigger) {
    patchState(this.state, stateUpdaterSelectTrigger(trigger));
    this.back();
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
