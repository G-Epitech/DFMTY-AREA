export enum AutomationSchemaFactValueType {
  STRING = 'String',
  DATETIME = 'Datetime',
}

export interface AutomationSchemaFactModel {
  name: string;
  description: string;
  type: string;
}
