import { inject, Injectable } from '@angular/core';
import { EditSheetService } from '@features/automations/workspace/components/edit-sheets/edit-sheet.service';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectActionSheetState,
  stateUpdaterSelectAction,
} from '@features/automations/workspace/components/edit-sheets/select-action-sheet/select-action-sheet.state';
import {
  ActionModel,
  ActionParameter,
  AutomationSchemaAction,
  AutomationSchemaModel,
  TriggerModel,
} from '@models/automation';
import {
  stateUpdaterSelectIntegration,
  stateUpdaterSelectLinkedIntegration,
} from '@features/automations/workspace/components/edit-sheets/edit-sheet.state.updaters';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';
import { firstValueFrom } from 'rxjs';
import { IntegrationsMediator } from '@mediators/integrations';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';

@Injectable()
export class SelectActionSheetService extends EditSheetService {
  readonly #paramEditService = inject(AutomationParameterEditService);
  readonly #workspaceStore = inject(AutomationWorkspaceStore);
  readonly #integrationsMediator = inject(IntegrationsMediator);

  state = signalState<SelectActionSheetState>({
    selectedAction: null,
    action: null,
    actionIndex: 0,
  });

  constructor() {
    super();
  }

  async initialize(
    automationAction: ActionModel | null,
    schema: AutomationSchemaModel,
    actionIndex: number
  ) {
    if (!automationAction) {
      return;
    }
    const integration = schema.getAvailableIntegrationByIdentifier(
      automationAction.integration
    );
    let linkedIntegration: IntegrationModel | null = null;
    if (automationAction.dependencies.length > 0) {
      const id = automationAction.dependencies[0];
      linkedIntegration = await firstValueFrom(
        this.#integrationsMediator.getById(id)
      );
    }

    const action = schema.getActionByIdentifier(
      automationAction.integration,
      automationAction.nameIdentifier
    );

    patchState(this.baseState, state => ({
      ...state,
      selectedIntegration: integration,
      selectedLinkedIntegration: linkedIntegration,
    }));
    patchState(this.state, state => ({
      ...state,
      actionIndex: actionIndex,
      selectedAction: action,
      action: automationAction,
    }));
  }

  selectParameter(
    parameter: ActionParameter,
    schema: AutomationSchemaModel | null
  ) {
    if (!schema) {
      return;
    }
    const parameterType = schema.getActionParameterType(
      this.baseState().selectedIntegration!.name,
      this.state().selectedAction!.name,
      parameter.identifier
    );
    patchState(this.baseState, state => ({
      ...state,
      selectedParameter: parameter,
      selecterParameterType: parameterType,
    }));
  }

  selectIntegration(integration: AvailableIntegrationType) {
    if (
      this.baseState().selectedIntegration?.identifier !==
      integration.identifier
    ) {
      patchState(this.state, state => ({
        ...state,
        selectedAction: null,
      }));
      patchState(this.baseState, state => ({
        ...state,
        selectedLinkedIntegration: null,
      }));
    }
    patchState(this.baseState, stateUpdaterSelectIntegration(integration));
    this.back();
  }

  selectLinkedIntegration(linkedIntegration: IntegrationModel) {
    patchState(
      this.baseState,
      stateUpdaterSelectLinkedIntegration(linkedIntegration)
    );
    patchState(this.state, state => {
      state.action!.addDependency(linkedIntegration.id);
      return state;
    });
    this.back();
  }

  selectAction(
    action: AutomationSchemaAction,
    schema: AutomationSchemaModel | null
  ) {
    if (this.state().selectedAction?.name !== action.name) {
      patchState(this.state, state => ({
        ...state,
        selectedLinkedIntegration: null,
      }));
    }

    patchState(this.state, stateUpdaterSelectAction(action));
    this.back();
    if (!schema) {
      return;
    }
    const currentIntegration = this.baseState().selectedIntegration!.name;
    const schemaActionIdentifier = schema.getActionIdentifier(
      currentIntegration,
      action.name
    );
    const params: ActionParameter[] = Object.keys(action.parameters).map(
      key => ({
        identifier: key,
        value: null,
        type: AutomationParameterFormatType.RAW,
      })
    );
    if (schemaActionIdentifier) {
      const identifier =
        this.baseState().selectedIntegration?.identifier +
        '.' +
        schemaActionIdentifier;
      const actionModel = new ActionModel(identifier, params, []);
      patchState(this.state, state => ({
        ...state,
        action: actionModel,
      }));
    }
  }

  getSelectedParameterDescription() {
    return this.state().selectedAction?.parameters[
      this.baseState().selectedParameter!.identifier
    ].description;
  }

  save() {
    const currentAction = this.state().action;
    if (!currentAction) {
      return;
    }

    const updatedAction = new TriggerModel(
      currentAction.identifier,
      this.#paramEditService.currentParameters(),
      [...currentAction.dependencies]
    );

    if (this.#workspaceStore.getAutomation()) {
      this.#workspaceStore.updateActions({
        idx: this.state().actionIndex,
        action: updatedAction,
      });
    }
  }
}
