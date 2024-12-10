export class AutomationModel {
  readonly id: string;
  readonly label: string;
  readonly description: string;
  readonly enabled: boolean;
  readonly lastUpdated: Date;
  readonly color: string;
  readonly iconName: string;
  readonly trigger?: string;
  readonly actions?: string[];

  constructor(
    id: string,
    label: string,
    description: string,
    enabled: boolean,
    lastUpdated: Date,
    color: string,
    iconName: string,
    trigger?: string,
    actions?: string[]
  ) {
    this.id = id;
    this.label = label;
    this.description = description;
    this.enabled = enabled;
    this.lastUpdated = lastUpdated;
    this.color = color;
    this.iconName = iconName;
    this.trigger = trigger;
    this.actions = actions;
  }
}
