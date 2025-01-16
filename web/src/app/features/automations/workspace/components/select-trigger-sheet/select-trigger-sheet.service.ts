import { inject, Injectable, OnDestroy } from '@angular/core';
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
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
  TriggerShortModel,
} from '@models/automation';
import { Subject, takeUntil } from 'rxjs';
import { IntegrationsMediator } from '@mediators/integrations';

@Injectable()
export class SelectTriggerSheetService implements OnDestroy {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #destroyRef = new Subject<void>();

  state = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
    selectedTrigger: null,
  });

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  initialize(
    automationTrigger: TriggerShortModel | null,
    schema: AutomationSchemaModel
  ) {
    if (!automationTrigger) {
      return;
    }
    const integration = schema.getAvailableIntegrationByName(
      automationTrigger.integration
    );
    let linkedIntegration: IntegrationModel | null = null;
    if (automationTrigger.providers.length > 0) {
      const id = automationTrigger.providers[0];
      this.#integrationsMediator
        .getById(id)
        .pipe(takeUntil(this.#destroyRef))
        .subscribe(integration => (linkedIntegration = integration));
    }
    const trigger = schema.getTriggerByIdentifier(
      automationTrigger.integration,
      automationTrigger.nameIdentifier
    );
    patchState(this.state, state => ({
      ...state,
      selectedIntegration: integration,
      selectedLinkedIntegration: linkedIntegration,
      selectedTrigger: trigger,
    }));
  }

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
    const lastSelectedIntegration = this.state().selectedIntegration;
    patchState(this.state, stateUpdaterSelectIntegration(integration));
    if (lastSelectedIntegration !== integration) {
      patchState(this.state, state => ({
        ...state,
        selectedLinkedIntegration: null,
        selectedTrigger: null,
      }));
    }
    this.back();
  }

  selectLinkedIntegration(linkedIntegration: IntegrationModel): void {
    const lastSelectedLinkedIntegration =
      this.state().selectedLinkedIntegration;
    patchState(
      this.state,
      stateUpdaterSelectLinkedIntegration(linkedIntegration)
    );
    if (lastSelectedLinkedIntegration !== linkedIntegration) {
      patchState(this.state, state => ({
        ...state,
        selectedTrigger: null,
      }));
    }

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
