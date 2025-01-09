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
}

export interface ActionDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationParameterDTO>;
  facts: Record<string, AutomationFactDTO>;
}

export interface AutomationServiceDTO {
  name: string;
  iconUri: string;
  color: string;
  triggers: Record<string, TriggerDTO>;
  actions: Record<string, ActionDTO>;
}

export type AutomationSchemaDTO = Record<string, AutomationServiceDTO>;
