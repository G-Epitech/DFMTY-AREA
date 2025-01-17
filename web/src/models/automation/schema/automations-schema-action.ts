import {
  AutomationSchemaDependency,
  AutomationSchemaFactModel,
  AutomationSchemaParameterModel,
} from '@models/automation';

export interface AutomationSchemaAction {
  name: string;
  description: string;
  icon: string;
  parameters: Record<string, AutomationSchemaParameterModel>;
  facts: Record<string, AutomationSchemaFactModel>;
  dependencies: Record<string, AutomationSchemaDependency>;
}
