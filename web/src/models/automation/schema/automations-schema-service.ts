import {
  AutomationSchemaAction,
  AutomationSchemaTrigger,
} from '@models/automation';

export interface AutomationSchemaService {
  name: string;
  color: string;
  iconUri: string;
  triggers: Record<string, AutomationSchemaTrigger>;
  actions: Record<string, AutomationSchemaAction>;
}
