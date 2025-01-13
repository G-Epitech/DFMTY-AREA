import { AutomationSchemaService } from '@models/automation/schema/automations-schema-service';

export class AutomationSchemaModel {
  readonly automationServices: Record<string, AutomationSchemaService>;

  constructor(services: Record<string, AutomationSchemaService>) {
    this.automationServices = services;
  }

  getIntegrationIconUri(integrationName: string) {
    return this.automationServices[integrationName.toLowerCase()].iconUri;
  }

  getIntegrationColor(integrationName: string) {
    return this.automationServices[integrationName.toLowerCase()].color;
  }
}
