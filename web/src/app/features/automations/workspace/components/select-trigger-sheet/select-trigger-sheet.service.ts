import { effect, inject, Injectable } from '@angular/core';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectTriggerSheetState,
  stateUpdaterSelectTrigger,
} from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.state';
import { IntegrationModel } from '@models/integration';
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
  TriggerModel,
  TriggerParameter,
} from '@models/automation';
import { firstValueFrom } from 'rxjs';
import { IntegrationsMediator } from '@mediators/integrations';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';
import {
  stateUpdaterSelectIntegration,
  stateUpdaterSelectLinkedIntegration,
} from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.updaters';
import { EditSheetService } from '@features/automations/workspace/components/edit-sheets/edit-sheet.service';
import { AvailableIntegrationType } from '@common/types';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

@Injectable()
export class SelectTriggerSheetService extends EditSheetService {
  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #paramEditService = inject(AutomationParameterEditService);
  readonly #workspaceStore = inject(AutomationWorkspaceStore);

  constructor() {
    super();
    effect(() => {
      if (
        this.state().selectedTrigger &&
        this.baseState().selectedLinkedIntegration &&
        this.baseState().selectedIntegration
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
    selectedTrigger: null,
    trigger: null,
  });

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

    patchState(this.baseState, state => ({
      ...state,
      selectedIntegration: integration,
      selectedLinkedIntegration: linkedIntegration,
    }));
    patchState(this.state, state => ({
      ...state,
      selectedTrigger: trigger,
      trigger: automationTrigger,
    }));
  }

  selectParameter(
    parameter: TriggerParameter,
    schema: AutomationSchemaModel | null
  ) {
    if (!schema) {
      return;
    }
    const parameterType = schema.getTriggerParameterType(
      this.baseState().selectedIntegration!.name,
      this.state().selectedTrigger!.name,
      parameter.identifier
    );
    patchState(this.baseState, state => ({
      ...state,
      selectedParameter: parameter,
      selecterParameterType: parameterType,
    }));
  }

  selectLinkedIntegration(linkedIntegration: IntegrationModel): void {
    patchState(
      this.baseState,
      stateUpdaterSelectLinkedIntegration(linkedIntegration)
    );
    patchState(this.state, state => {
      state.trigger!.addDependency(linkedIntegration.id);
      return state;
    });
    this.back();
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    if (
      this.baseState().selectedIntegration?.identifier !==
      integration.identifier
    ) {
      patchState(this.state, state => ({
        ...state,
        selectedTrigger: null,
      }));
      patchState(this.baseState, state => ({
        ...state,
        selectedLinkedIntegration: null,
      }));
    }
    patchState(this.baseState, stateUpdaterSelectIntegration(integration));
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
    const currentIntegration = this.baseState().selectedIntegration!.name;
    const schemaTriggerIdentifier = schema.getTriggerIdentifier(
      currentIntegration,
      trigger.name
    );
    const params: TriggerParameter[] = Object.keys(trigger.parameters).map(
      key => ({
        identifier: key,
        value: null,
        type: AutomationParameterFormatType.RAW,
      })
    );
    if (schemaTriggerIdentifier) {
      const identifier =
        this.baseState().selectedIntegration?.identifier +
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
      this.baseState().selectedParameter!.identifier
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
}
