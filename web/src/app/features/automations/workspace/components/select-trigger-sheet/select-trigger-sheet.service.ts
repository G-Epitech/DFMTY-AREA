import { effect, inject, Injectable, OnDestroy, signal } from '@angular/core';
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
  TriggerParameter,
} from '@models/automation';
import { firstValueFrom, Subject } from 'rxjs';
import { IntegrationsMediator } from '@mediators/integrations';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';

@Injectable()
export class SelectTriggerSheetService implements OnDestroy {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #destroyRef = new Subject<void>();
  readonly #paramEditService = inject(AutomationParameterEditService);
  readonly #workspaceStore = inject(AutomationWorkspaceStore);

  constructor() {
    effect(() => {
      if (
        this.state().selectedTrigger &&
        this.state().selectedLinkedIntegration &&
        this.state().selectedIntegration
      ) {
        this.saveDisabled.set(false);
        const params = this.#paramEditService.currentParameters();
        this.valid.set(true);
        for (const param of params) {
          if (param.value === null) {
            this.valid.set(false);
            break;
          }
        }
      } else {
        this.saveDisabled.set(true);
        this.valid.set(null);
      }
    });
  }

  state = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
    selectedLinkedIntegration: null,
    selectedTrigger: null,
    trigger: null,
    selectedParameter: null,
    selecterParameterType: null,
  });
  valid = signal<boolean | null>(null);
  saveDisabled = signal<boolean>(true);

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  async initialize(
    automationTrigger: TriggerModel | null,
    schema: AutomationSchemaModel
  ) {
    if (!automationTrigger) {
      return;
    }
    const integration = schema.getAvailableIntegrationByIdentifier(
      automationTrigger.integration
    );
    let linkedIntegration: IntegrationModel | null = null;
    if (automationTrigger.dependencies.length > 0) {
      const id = automationTrigger.dependencies[0];
      linkedIntegration = await firstValueFrom(
        this.#integrationsMediator.getById(id)
      );
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
      trigger: automationTrigger,
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

  goToParameterEdit() {
    patchState(this.state, stateUpdaterGoTo(TriggerSelectionStep.PARAMETER));
  }

  selectParameter(
    parameter: TriggerParameter,
    schema: AutomationSchemaModel | null
  ) {
    if (!schema) {
      return;
    }
    const parameterType = schema.getTriggerParameterType(
      this.state().selectedIntegration!.name,
      this.state().selectedTrigger!.name,
      parameter.identifier
    );
    patchState(this.state, state => ({
      ...state,
      selectedParameter: parameter,
      selecterParameterType: parameterType,
    }));
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
    patchState(this.state, state => {
      state.trigger!.addDependency(linkedIntegration.id);
      return state;
    });
    this.back();
  }

  selectTrigger(
    trigger: AutomationSchemaTrigger,
    schema: AutomationSchemaModel | null
  ) {
    if (this.state().selectedTrigger?.name !== trigger.name) {
      patchState(this.state, state => ({
        ...state,
        selectedLinkedIntegration: null,
      }));
    }
    patchState(this.state, stateUpdaterSelectTrigger(trigger));
    this.back();
    if (!schema) {
      return;
    }
    const currentIntegration = this.state().selectedIntegration!.name;
    const schemaTriggerIdentifier = schema.getTriggerIdentifier(
      currentIntegration,
      trigger.name
    );
    const params: TriggerParameter[] = Object.keys(trigger.parameters).map(
      key => ({
        identifier: key,
        value: null,
      })
    );
    if (schemaTriggerIdentifier) {
      const identifier =
        this.state().selectedIntegration?.identifier +
        '.' +
        schemaTriggerIdentifier;
      const triggerShort = new TriggerModel(identifier, params, []);
      patchState(this.state, state => ({
        ...state,
        trigger: triggerShort,
      }));
    }
  }

  getSelectedParameterDescription(): string | undefined {
    return this.state().selectedTrigger?.parameters[
      this.state().selectedParameter!.identifier
    ].description;
  }

  save() {
    const currentTrigger = this.state().trigger;
    if (!currentTrigger) {
      return;
    }

    const updatedTrigger = new TriggerModel(
      currentTrigger.identifier,
      this.#paramEditService.currentParameters(),
      [...currentTrigger.dependencies]
    );

    if (this.#workspaceStore.getAutomation()) {
      this.#workspaceStore.addTrigger(updatedTrigger);
    }
  }

  back() {
    patchState(this.state, stateUpdaterBack());
  }
}
