import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { AutomationModel } from '@models/automation.model';
import { NgIcon } from '@ng-icons/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'tr-automation-card',
  imports: [NgIcon, NgClass],
  templateUrl: './automation-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationCardComponent {
  automation = input.required<AutomationModel>();
}
