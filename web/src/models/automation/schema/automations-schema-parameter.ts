export enum AutomationSchemaParameterValueType {
  STRING = 'String',
  DATETIME = 'Datetime',
}

export interface AutomationSchemaParameterModel {
  name: string;
  description: string;
  type: string;
}
