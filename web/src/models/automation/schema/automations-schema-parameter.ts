import { AutomationParameterType } from '@models/automation';

export interface AutomationSchemaParameterModel {
  name: string;
  description: string;
  type: AutomationParameterType;
}
