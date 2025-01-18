export interface AutomationSchemaDependencyDTO {
  require: string;
  optional: boolean;
}

export interface AutomationSchemaParameterDTO {
  name: string;
  description: string;
  type: string;
}

export interface AutomationSchemaFactDTO {
  name: string;
  description: string;
  type: string;
}

export interface AutomationSchemaTriggerDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationSchemaParameterDTO>;
  facts: Record<string, AutomationSchemaFactDTO>;
  dependencies: Record<string, AutomationSchemaDependencyDTO>;
}

export interface AutomationSchemaActionDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationSchemaParameterDTO>;
  facts: Record<string, AutomationSchemaFactDTO>;
  dependencies: Record<string, AutomationSchemaDependencyDTO>;
}

export interface AutomationServiceDTO {
  name: string;
  iconUri: string;
  color: string;
  triggers: Record<string, AutomationSchemaTriggerDTO>;
  actions: Record<string, AutomationSchemaActionDTO>;
}

export type AutomationSchemaDTO = Record<string, AutomationServiceDTO>;
