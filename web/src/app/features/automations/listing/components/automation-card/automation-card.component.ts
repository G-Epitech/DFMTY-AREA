import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { AutomationModel } from '@models/automation.model';
import { NgIcon } from '@ng-icons/core';
import { NgClass, NgStyle } from '@angular/common';
import { FormatDatePipe } from '@app/pipes';
import { RouterLink } from '@angular/router';
import { iconName } from '@utils/icon';

@Component({
  selector: 'tr-automation-card',
  imports: [NgIcon, NgClass, FormatDatePipe, NgStyle, RouterLink],
  templateUrl: './automation-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationCardComponent {
  automation = input.required<AutomationModel>();

  icon = computed(() => {
    return iconName(this.automation().iconName);
  });
}
