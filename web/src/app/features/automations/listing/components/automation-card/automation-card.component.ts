import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { AutomationModel } from '@models/automation.model';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { FormatDatePipe } from '@app/pipes';

@Component({
  selector: 'tr-automation-card',
  imports: [NgIcon, FormatDatePipe, NgStyle],
  templateUrl: './automation-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationCardComponent {
  automation = input.required<AutomationModel>();

  iconName = computed(() => {
    const words = this.automation().iconName.split('-');

    const camelCase = words
      .map((word, index) =>
        index === 0 ? word : word.charAt(0).toUpperCase() + word.slice(1)
      )
      .join('');

    return `hero${camelCase.charAt(0).toUpperCase() + camelCase.slice(1)}Solid`;
  });
}
