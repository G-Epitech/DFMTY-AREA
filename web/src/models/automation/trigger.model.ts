export interface TriggerParameter {
  identifier: string;
  value: string | null;
}

export class TriggerModel {
  readonly identifier: string;
  readonly parameters: TriggerParameter[];
  dependencies: string[];

  constructor(
    identifier: string,
    parameters: TriggerParameter[],
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
