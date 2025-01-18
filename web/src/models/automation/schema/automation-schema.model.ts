import { AutomationSchemaService } from '@models/automation/schema/automations-schema-service';
import { AvailableIntegrationType } from '@common/types';
import {
  ActionModel,
  AutomationParameterValueType,
  AutomationSchemaAction,
  AutomationSchemaFactModel,
  AutomationSchemaTrigger,
  TriggerModel,
} from '@models/automation';

export class AutomationSchemaModel {
  readonly automationServices: Record<string, AutomationSchemaService>;

  constructor(services: Record<string, AutomationSchemaService>) {
    this.automationServices = services;
  }

  static searchAvailableIntegrations(
    availableIntegrations: AvailableIntegrationType[],
    query: string
  ): AvailableIntegrationType[] {
    const searchTerm = query.toLowerCase();

    if (!searchTerm) return availableIntegrations;

    return availableIntegrations.filter(
      integration =>
        integration.name.toLowerCase().includes(searchTerm) ||
        integration.triggers.some(trigger =>
          trigger.toLowerCase().includes(searchTerm)
        ) ||
        integration.actions.some(action =>
          action.toLowerCase().includes(searchTerm)
        )
    );
  }

  getAvailableIntegration(): AvailableIntegrationType[] {
    return Object.entries(this.automationServices).map(
      ([name, integration]) => ({
        color: integration.color,
        name: integration.name,
        iconUri: integration.iconUri,
        identifier: name,
        triggers: Object.entries(integration.triggers).map(
          ([, trigger]) => trigger.name
        ),
        actions: Object.entries(integration.actions).map(
          ([, action]) => action.name
        ),
      })
    );
  }

  getIntegrationIconUri(integrationName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name === integrationName) {
        return value.iconUri;
      }
    }
    return null;
  }

  getIntegrationColor(integrationName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name === integrationName) {
        return value.color;
      }
    }
    return null;
  }

  getTriggerIcon(integrationName: string, triggerName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, trigger] of Object.entries(value.triggers)) {
          if (key === triggerName) {
            return trigger.icon;
          }
        }
      }
    }
    return null;
  }

  getTriggerName(integrationName: string, triggerName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, trigger] of Object.entries(value.triggers)) {
          if (key === triggerName) {
            return trigger.name;
          }
        }
      }
    }
    return null;
  }

  getActionIcon(integrationName: string, actionName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, action] of Object.entries(value.actions)) {
          if (key === actionName) {
            return action.icon;
          }
        }
      }
    }
    return null;
  }

  getActionName(integrationName: string, actionName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, action] of Object.entries(value.actions)) {
          if (key === actionName) {
            return action.name;
          }
        }
      }
    }
    return null;
  }

  getAvailableTriggers(integrationName: string): AutomationSchemaTrigger[] {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        return Object.values(value.triggers);
      }
    }
    return [];
  }

  getAvailableActions(integrationName: string): AutomationSchemaAction[] {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        return Object.values(value.actions);
      }
    }
    return [];
  }

  getTriggerByIdentifier(
    integrationName: string,
    triggerIdentifier: string
  ): AutomationSchemaTrigger | null {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        return value.triggers[triggerIdentifier];
      }
    }
    return null;
  }

  getActionByIdentifier(
    integrationName: string,
    actionIdentifier: string
  ): AutomationSchemaAction | null {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        return value.actions[actionIdentifier];
      }
    }
    return null;
  }

  getAvailableIntegrationByName(
    integrationName: string
  ): AvailableIntegrationType | null {
    for (const [key, value] of Object.entries(this.automationServices)) {
      if (value.name === integrationName) {
        return {
          color: value.color,
          name: value.name,
          iconUri: value.iconUri,
          identifier: key,
          triggers: Object.entries(value.triggers).map(
            ([, trigger]) => trigger.name
          ),
          actions: Object.entries(value.actions).map(
            ([, action]) => action.name
          ),
        };
      }
    }
    return null;
  }

  getAvailableIntegrationByIdentifier(
    integrationIdentifier: string
  ): AvailableIntegrationType | null {
    const value = this.automationServices[integrationIdentifier];
    if (!value) {
      return null;
    }
    return {
      color: value.color,
      name: value.name,
      iconUri: value.iconUri,
      identifier: integrationIdentifier,
      triggers: Object.entries(value.triggers).map(
        ([, trigger]) => trigger.name
      ),
      actions: Object.entries(value.actions).map(([, action]) => action.name),
    };
  }

  getTriggerIdentifier(integrationName: string, triggerName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, trigger] of Object.entries(value.triggers)) {
          if (trigger.name === triggerName) {
            return key;
          }
        }
      }
    }
    return null;
  }

  getActionIdentifier(integrationName: string, actionName: string) {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [key, action] of Object.entries(value.actions)) {
          if (action.name === actionName) {
            return key;
          }
        }
      }
    }
    return null;
  }

  getTriggerParameterType(
    integrationName: string,
    triggerName: string,
    parameterName: string
  ): AutomationParameterValueType {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [, trigger] of Object.entries(value.triggers)) {
          if (trigger.name === triggerName) {
            return trigger.parameters[parameterName].type;
          }
        }
      }
    }
    return AutomationParameterValueType.STRING;
  }

  getActionParameterType(
    integrationName: string,
    actionName: string,
    parameterName: string
  ): AutomationParameterValueType {
    for (const [, value] of Object.entries(this.automationServices)) {
      if (value.name == integrationName) {
        for (const [, action] of Object.entries(value.actions)) {
          if (action.name === actionName) {
            return action.parameters[parameterName].type;
          }
        }
      }
    }
    return AutomationParameterValueType.STRING;
  }

  getTriggerFacts(
    trigger: TriggerModel
  ): Record<string, AutomationSchemaFactModel> | null {
    const integration = trigger.integration;
    const triggerIdentifier = trigger.nameIdentifier;
    return this.automationServices[integration].triggers[triggerIdentifier]
      .facts;
  }

  getActionFacts(
    action: ActionModel
  ): Record<string, AutomationSchemaFactModel> | null {
    const integration = action.integration;
    const actionIdentifier = action.nameIdentifier;
    return this.automationServices[integration].actions[actionIdentifier].facts;
  }
}
