import { computed, Injectable, signal } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import { AutomationStepSelectionStep } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { EditSheetStateInterface } from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.interface';
import {
  stateUpdaterBack,
  stateUpdaterGoTo,
  stateUpdaterSelectIntegration,
} from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.updaters';
import { AvailableIntegrationType } from '@common/types';

@Injectable({
  providedIn: 'root',
})
export class EditSheetService {
  valid = signal<boolean | null>(null);
  saveDisabled = signal<boolean>(true);

  baseState = signalState<EditSheetStateInterface>({
    selectionStep: AutomationStepSelectionStep.MAIN,
    stepHistory: [AutomationStepSelectionStep.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
  });

  title = computed(() => {
    const step = this.baseState().selectionStep;
    const titles: Record<AutomationStepSelectionStep, string> = {
      [AutomationStepSelectionStep.MAIN]: 'Edit Event',
      [AutomationStepSelectionStep.INTEGRATION]: 'Select Integration',
      [AutomationStepSelectionStep.LINKED_INTEGRATION]:
        'Select Linked Integration',
      [AutomationStepSelectionStep.STEP]: 'Select Event',
      [AutomationStepSelectionStep.PARAMETER]: '',
    };
    return titles[step];
  });

  description = computed(() => {
    const step = this.baseState().selectionStep;
    const descriptions: Record<AutomationStepSelectionStep, string> = {
      [AutomationStepSelectionStep.MAIN]: 'Edit the event for this automation',
      [AutomationStepSelectionStep.INTEGRATION]: 'Choose an integration to use',
      [AutomationStepSelectionStep.LINKED_INTEGRATION]:
        'Select a linked account',
      [AutomationStepSelectionStep.STEP]: 'Choose an event',
      [AutomationStepSelectionStep.PARAMETER]: '',
    };
    return descriptions[step];
  });

  goToIntegrationSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionStep.INTEGRATION)
    );
  }

  goToLinkedInegrationSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionStep.LINKED_INTEGRATION)
    );
  }

  goToStepSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionStep.STEP)
    );
  }

  goToParameterEdit() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionStep.PARAMETER)
    );
  }

  back() {
    patchState(this.baseState, stateUpdaterBack());
  }
}
