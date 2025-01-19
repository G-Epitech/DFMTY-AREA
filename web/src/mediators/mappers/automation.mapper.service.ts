import { Injectable } from '@angular/core';
import {
  ActionDTO,
  AutomationDTO,
  AutomationSchemaActionDTO,
  AutomationSchemaDependencyDTO,
  AutomationSchemaParameterDTO,
  AutomationSchemaTriggerDTO,
  TriggerDTO,
} from '@repositories/automations/dto';
import {
  ActionModel,
  ActionParameter,
  AutomationModel,
  AutomationParameterValueType,
  AutomationSchemaDependency,
  AutomationSchemaDependencyRequired,
  AutomationSchemaParameterModel,
  AutomationSchemaTrigger,
  TriggerModel,
  TriggerParameter,
} from '@models/automation';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

@Injectable({
  providedIn: 'root',
})
export class AutomationMapperService {
  mapSchemaServiceTriggers(
    dtoTriggers: Record<string, AutomationSchemaTriggerDTO>
  ): Record<string, AutomationSchemaTrigger> {
    const schemaTriggers: Record<string, AutomationSchemaTrigger> = {};

    for (const [identifier, trigger] of Object.entries(dtoTriggers)) {
      schemaTriggers[identifier] = {
        name: trigger.name,
        description: trigger.description,
        icon: trigger.icon,
        parameters: this._mapSchemaParameters(trigger.parameters),
        facts: trigger.facts,
        dependencies: this._mapSchemaDependencies(trigger.dependencies),
      };
    }
    return schemaTriggers;
  }

  mapSchemaServiceActions(
    dtoActions: Record<string, AutomationSchemaActionDTO>
  ): Record<string, AutomationSchemaTrigger> {
    const schemaActions: Record<string, AutomationSchemaTrigger> = {};

    for (const [identifier, action] of Object.entries(dtoActions)) {
      schemaActions[identifier] = {
        name: action.name,
        description: action.description,
        icon: action.icon,
        parameters: this._mapSchemaParameters(action.parameters),
        facts: action.facts,
        dependencies: this._mapSchemaDependencies(action.dependencies),
      };
    }
    return schemaActions;
  }

  _mapSchemaDependencies(
    dependencies: Record<string, AutomationSchemaDependencyDTO>
  ): Record<string, AutomationSchemaDependency> {
    const schemaDependencies: Record<string, AutomationSchemaDependency> = {};

    for (const [identifier, dependency] of Object.entries(dependencies)) {
      schemaDependencies[identifier] = {
        require: dependency.require as AutomationSchemaDependencyRequired,
        optional: dependency.optional,
      };
    }
    return schemaDependencies;
  }

  _mapSchemaParameters(
    parameters: Record<string, AutomationSchemaParameterDTO>
  ): Record<string, AutomationSchemaParameterModel> {
    const schemaParameters: Record<string, AutomationSchemaParameterModel> = {};

    for (const [identifier, parameter] of Object.entries(parameters)) {
      schemaParameters[identifier] = {
        name: parameter.name,
        description: parameter.description,
        type: parameter.type as AutomationParameterValueType,
      };
    }
    return schemaParameters;
  }

  _mapActionParameters(
    parameters: { type: string; identifier: string; value: string }[]
  ): ActionParameter[] {
    return parameters.map(parameter => {
      return {
        type: parameter.type as AutomationParameterFormatType,
        identifier: parameter.identifier,
        value: parameter.value,
      } as ActionParameter;
    });
  }

  mapAutomationModel(dto: AutomationDTO): AutomationModel {
    return new AutomationModel(
      dto.id,
      dto.ownerId,
      dto.label,
      dto.description,
      dto.enabled,
      dto.updatedAt,
      '#EE883A',
      'chat-bubble-bottom-center-text',
      this.mapTriggerModel(dto.trigger),
      this.mapActionsModel(dto.actions)
    );
  }

  mapActionsModel(actions: ActionDTO[]): ActionModel[] {
    return actions.map(
      action =>
        new ActionModel(
          action.identifier,
          this._mapActionParameters(action.parameters),
          action.dependencies
        )
    );
  }

  mapTriggerModel(trigger: TriggerDTO): TriggerModel {
    return new TriggerModel(
      trigger.identifier,
      this._mapTriggerModelParameters(trigger.parameters),
      trigger.dependencies
    );
  }

  _mapTriggerModelParameters(
    parameters: { identifier: string; value: string }[]
  ): TriggerParameter[] {
    return parameters.map(parameter => {
      return {
        type: AutomationParameterFormatType.RAW,
        identifier: parameter.identifier,
        value: parameter.value,
      } as TriggerParameter;
    });
  }
}
