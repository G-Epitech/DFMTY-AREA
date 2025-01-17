import { Injectable } from '@angular/core';
import {
  ActionDTO,
  AutomationSchemaActionDTO,
  AutomationSchemaDependencyDTO,
  AutomationSchemaParameterDTO,
  AutomationSchemaTriggerDTO,
} from '@repositories/automations/dto';
import {
  ActionModel,
  ActionParameter,
  AutomationParameterType,
  AutomationSchemaDependency,
  AutomationSchemaDependencyRequired,
  AutomationSchemaParameterModel,
  AutomationSchemaTrigger,
} from '@models/automation';

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

  mapActions(actions: ActionDTO[]): ActionModel[] {
    return actions.map(
      action =>
        new ActionModel(
          action.identifier,
          this._mapActionParameters(action.parameters),
          action.dependencies
        )
    );
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
        type: parameter.type as AutomationParameterType,
      };
    }
    return schemaParameters;
  }

  _mapActionParameters(
    parameters: { type: string; identifier: string; value: string }[]
  ): ActionParameter[] {
    return parameters.map(parameter => {
      return {
        type: parameter.type as AutomationParameterType,
        identifier: parameter.identifier,
        value: parameter.value,
      } as ActionParameter;
    });
  }
}
