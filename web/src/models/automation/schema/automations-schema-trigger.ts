import {
  AutomationSchemaFactModel,
  AutomationSchemaParameterModel,
} from '@models/automation';

export interface AutomationSchemaTrigger {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationSchemaParameterModel>;
  facts: Record<string, AutomationSchemaFactModel>;
}
