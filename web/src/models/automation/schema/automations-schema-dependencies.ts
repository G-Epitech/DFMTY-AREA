export enum AutomationSchemaDependencyRequired {
  SINGLE = 'Single',
  MULTIPLE = 'Multiple',
}

export interface AutomationSchemaDependency {
  require: AutomationSchemaDependencyRequired;
  optional: boolean;
}
