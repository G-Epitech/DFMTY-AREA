import { TriggerShortModel } from '@models/automation/trigger-short.model';
import { ActionShortModel } from '@models/automation/action-short.model';
import { iconNameFromIdentifier } from '@utils/icon';

export class AutomationModel {
  readonly id: string;
  readonly ownerId: string;
  readonly label: string;
  readonly description: string;
  readonly enabled: boolean;
  readonly updatedAt: Date;
  readonly color: string;
  readonly #iconName: string;
  readonly trigger: TriggerShortModel | null;
  readonly actions: ActionShortModel[];

  constructor(
    id: string,
    ownerId: string,
    label: string,
    description: string,
    enabled: boolean,
    updatedAt: Date,
    color: string,
    iconName: string,
    trigger: TriggerShortModel | null,
    actions: ActionShortModel[]
  ) {
    this.id = id;
    this.ownerId = ownerId;
    this.label = label;
    this.description = description;
    this.enabled = enabled;
    this.updatedAt = updatedAt;
    this.color = color;
    this.#iconName = iconName;
    this.trigger = trigger;
    this.actions = actions;
  }

  get hasTrigger(): boolean {
    return !!this.trigger;
  }

  get iconName(): string {
    return iconNameFromIdentifier(this.#iconName);
  }
}

export function newDefaultAutomationModel(): AutomationModel {
  return new AutomationModel(
    '',
    '',
    'New Automation',
    'New Automation Description',
    true,
    new Date(),
    '#EE883A',
    'bolt',
    null,
    []
  );
}
