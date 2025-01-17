export class TriggerShortModel {
  readonly identifier: string;
  readonly parameters: { identifier: string; value: string }[];
  readonly dependencies: string[];

  constructor(
    identifier: string,
    parameters: { identifier: string; value: string }[],
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
}
