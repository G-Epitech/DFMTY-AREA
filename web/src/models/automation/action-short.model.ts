export class ActionShortModel {
  readonly identifier: string;
  readonly parameters: { type: string; identifier: string; value: string }[];
  readonly providers: string[];

  constructor(
    identifier: string,
    parameters: { type: string; identifier: string; value: string }[],
    providers: string[]
  ) {
    this.identifier = identifier;
    this.parameters = parameters;
    this.providers = providers;
  }

  get integration(): string {
    return this.identifier.split('.')[0];
  }

  get nameIdentifier(): string {
    return this.identifier.split('.')[1];
  }
}
