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
  TriggerModel,
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
    trigger: null,
  });

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  initialize(
    automationTrigger: TriggerModel | null,
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
    if (this.state().selectedLinkedIntegration?.id !== linkedIntegration.id) {
      patchState(this.state, state => ({
        ...state,
        selectedTrigger: null,
      }));
    }
    patchState(
      this.state,
      stateUpdaterSelectLinkedIntegration(linkedIntegration)
    );

    this.back();
  }

  selectTrigger(
    trigger: AutomationSchemaTrigger,
    schema: AutomationSchemaModel | null
  ) {
    patchState(this.state, stateUpdaterSelectTrigger(trigger));
    this.back();
    if (!schema) {
      return;
    }
    const currentIntegration = this.state().selectedIntegration!.name;
    const triggerIdentifer = schema.getTriggerIdentifier(
      currentIntegration,
      trigger.name
    );
    if (triggerIdentifer) {
      const dependency = this.state().selectedLinkedIntegration!.id;
      const triggerShort = new TriggerModel(
        triggerIdentifer,
        [],
        dependency ? [dependency] : []
      );
      patchState(this.state, state => ({
        ...state,
        trigger: triggerShort,
      }));
    }
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
