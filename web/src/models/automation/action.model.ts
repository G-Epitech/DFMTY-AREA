import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

export interface ActionParameter {
  readonly type: AutomationParameterFormatType;
  readonly identifier: string;
  readonly value: string | null;
}

export class ActionModel {
  readonly identifier: string;
  readonly parameters: ActionParameter[];
  readonly dependencies: string[];

  constructor(
    identifier: string,
    parameters: ActionParameter[],
    dependencies: string[]
  ) {
    this.identifier = identifier;
    this.parameters = parameters;
    this.dependencies = dependencies;
  }

  get integration(): string {
    return this.identifier.split('.')[0];
  }

  get nameIdentifier(): string {
    return this.identifier.split('.')[1];
  }

  addDependency(dependency: string): void {
    this.dependencies.push(dependency);
  }
}
