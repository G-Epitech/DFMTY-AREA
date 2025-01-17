export interface DependencyDTO {
  require: string;
  optional: boolean;
}

export interface AutomationParameterDTO {
  name: string;
  description: string;
  type: string;
}

export interface AutomationFactDTO {
  name: string;
  description: string;
  type: string;
}

export interface TriggerDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationParameterDTO>;
  facts: Record<string, AutomationFactDTO>;
  dependencies: Record<string, DependencyDTO>;
}

export interface ActionDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationParameterDTO>;
  facts: Record<string, AutomationFactDTO>;
  dependencies: Record<string, DependencyDTO>;
}

export interface AutomationServiceDTO {
  name: string;
  iconUri: string;
  color: string;
  triggers: Record<string, TriggerDTO>;
  actions: Record<string, ActionDTO>;
}

export type AutomationSchemaDTO = Record<string, AutomationServiceDTO>;
