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
  selector: 'tr-automation-step-selection-button',
  imports: [NgIcon, NgStyle],
  templateUrl: './automation-step-selection-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationStepSelectionButtonComponent {
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
