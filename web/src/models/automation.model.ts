export class AutomationModel {
  readonly id: string;
  readonly label: string;
  readonly description: string;
  readonly enabled: boolean;
  readonly lastUpdated: Date;
  readonly trigger?: string;
  readonly actions?: string[];

  constructor(
    id: string,
    label: string,
    description: string,
    enabled: boolean,
    lastUpdated: Date,
    trigger?: string,
    actions?: string[]
  ) {
    this.id = id;
    this.label = label;
    this.description = description;
    this.enabled = enabled;
    this.lastUpdated = lastUpdated;
    this.trigger = trigger;
    this.actions = actions;
  }
}
