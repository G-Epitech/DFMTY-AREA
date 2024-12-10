export interface AutomationParameterDTO {
  name: string;
  description: string;
  type: string;
}

export interface TriggerOrActionDTO {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationParameterDTO>;
  facts: Record<string, AutomationParameterDTO>;
}

export interface AutomationServiceDTO {
  name: string;
  iconUri: string;
  color: string;
  triggers: Record<string, TriggerOrActionDTO>;
  actions: Record<string, TriggerOrActionDTO>;
}

export type AutomationManifestDTO = Record<string, AutomationServiceDTO>;
