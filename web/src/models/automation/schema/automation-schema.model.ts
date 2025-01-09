import { AutomationSchemaService } from '@models/automation/schema/automations-schema-service';

export class AutomationSchemaModel {
  readonly automationServices: Record<string, AutomationSchemaService>;

  constructor(services: Record<string, AutomationSchemaService>) {
    this.automationServices = services;
  }
}
