import { AutomationSchemaService } from '@models/automation/schema/automations-schema-service';

export class AutomationSchemaModel {
  readonly automationServices: Record<string, AutomationSchemaService>;

  constructor(services: Record<string, AutomationSchemaService>) {
    this.automationServices = services;
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
    return null;  }
}
