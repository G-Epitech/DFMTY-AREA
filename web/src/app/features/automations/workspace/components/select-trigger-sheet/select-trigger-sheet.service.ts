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
    if (automationTrigger.dependencies.length > 0) {
      const id = automationTrigger.dependencies[0];
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
    if (
      this.state().selectedIntegration?.identifier !== integration.identifier
    ) {
      patchState(this.state, state => ({
        ...state,
        selectedLinkedIntegration: null,
        selectedTrigger: null,
      }));
    }
    patchState(this.state, stateUpdaterSelectIntegration(integration));
    this.back();
  }

  selectLinkedIntegration(linkedIntegration: IntegrationModel): void {
    patchState(
      this.state,
      stateUpdaterSelectLinkedIntegration(linkedIntegration)
    );

    this.back();
  }

  selectTrigger(trigger: AutomationSchemaTrigger) {
    if (this.state().selectedTrigger?.name !== trigger.name) {
      patchState(this.state, state => ({
        ...state,
        selectedLinkedIntegration: null,
      }));
    }
    patchState(this.state, stateUpdaterSelectTrigger(trigger));
    this.back();
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
