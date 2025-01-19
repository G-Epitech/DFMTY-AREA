export interface AutomationCreateTriggerParameterDTO {
  identifier: string;
  value: string;
}

export interface AutomationCreateTriggerDTO {
  identifier: string;
  parameters: AutomationCreateTriggerParameterDTO[];
  dependencies: string[];
}

export interface AutomationCreateActionParameterDTO {
  identifier: string;
  value: string;
  type: string;
}

export interface AutomationCreateActionDTO {
  identifier: string;
  parameters: AutomationCreateActionParameterDTO[];
  dependencies: string[];
}

export interface AutomationCreateDTO {
  label: string;
  description: string;
  color: string;
  icon: string;
  trigger: AutomationCreateTriggerDTO;
  actions: AutomationCreateActionDTO[];
  enabled: boolean;
}
