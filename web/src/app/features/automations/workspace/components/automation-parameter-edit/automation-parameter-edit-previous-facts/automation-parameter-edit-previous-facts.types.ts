import { AutomationParameterValueType } from '@models/automation';

export interface DeepAutomationFact {
  eventIdentifier: string;
  identifier: string;
  name: string;
  description: string;
  type: AutomationParameterValueType;
  idx: number | null;
}
