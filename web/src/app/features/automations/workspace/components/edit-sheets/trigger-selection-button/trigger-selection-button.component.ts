import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  signal,
} from '@angular/core';
import {
  AutomationSchemaAction,
  AutomationSchemaModel,
  AutomationSchemaTrigger,
} from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { iconNameFromIdentifier } from '@utils/icon';

@Component({
  selector: 'tr-trigger-selection-button',
  imports: [NgIcon, NgStyle],
  templateUrl: './trigger-selection-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class TriggerSelectionButtonComponent {
  schema: AutomationSchemaModel | null | undefined = null;

  automationEvent = input.required<
    AutomationSchemaTrigger | AutomationSchemaAction
  >();
  color = input.required<string | null>();

  icon = signal<string>('');

  constructor() {
    effect(() => {
      this.icon.set(iconNameFromIdentifier(this.automationEvent().icon));
    });
  }
}
