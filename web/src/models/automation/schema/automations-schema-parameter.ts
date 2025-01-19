import { AutomationParameterValueType } from '@models/automation';

export interface AutomationSchemaParameterModel {
  name: string;
  description: string;
  type: AutomationParameterValueType;
}
