import { computed, Injectable, signal } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import { AutomationStepSelectionType } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { EditSheetStateInterface } from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.interface';
import {
  stateUpdaterBack,
  stateUpdaterGoTo,
} from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.updaters';

@Injectable({
  providedIn: 'root',
})
export class EditSheetService {
  valid = signal<boolean | null>(null);
  saveDisabled = signal<boolean>(true);

  baseState = signalState<EditSheetStateInterface>({
    selectionStep: AutomationStepSelectionType.MAIN,
    stepHistory: [AutomationStepSelectionType.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
    selectedParameter: null,
    selecterParameterType: null,
  });

  title = computed(() => {
    const step = this.baseState().selectionStep;
    const titles: Record<AutomationStepSelectionType, string> = {
      [AutomationStepSelectionType.MAIN]: 'Edit Event',
      [AutomationStepSelectionType.INTEGRATION]: 'Select Integration',
      [AutomationStepSelectionType.LINKED_INTEGRATION]:
        'Select Linked Integration',
      [AutomationStepSelectionType.STEP]: 'Select Event',
      [AutomationStepSelectionType.PARAMETER]: '',
    };
    return titles[step];
  });

  description = computed(() => {
    const step = this.baseState().selectionStep;
    const descriptions: Record<AutomationStepSelectionType, string> = {
      [AutomationStepSelectionType.MAIN]: 'Edit the event for this automation',
      [AutomationStepSelectionType.INTEGRATION]: 'Choose an integration to use',
      [AutomationStepSelectionType.LINKED_INTEGRATION]:
        'Select a linked account',
      [AutomationStepSelectionType.STEP]: 'Choose an event',
      [AutomationStepSelectionType.PARAMETER]: '',
    };
    return descriptions[step];
  });

  goToIntegrationSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionType.INTEGRATION)
    );
  }

  goToLinkedInegrationSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionType.LINKED_INTEGRATION)
    );
  }

  goToStepSelection() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionType.STEP)
    );
  }

  goToParameterEdit() {
    patchState(
      this.baseState,
      stateUpdaterGoTo(AutomationStepSelectionType.PARAMETER)
    );
  }

  back() {
    patchState(this.baseState, stateUpdaterBack());
  }
}
