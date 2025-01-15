import { AutomationSchemaService } from '@models/automation/schema/automations-schema-service';
import { AvailableIntegrationType } from '@common/types';

export class AutomationSchemaModel {
  readonly automationServices: Record<string, AutomationSchemaService>;

  constructor(services: Record<string, AutomationSchemaService>) {
    this.automationServices = services;
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
}
