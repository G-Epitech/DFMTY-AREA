import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { AutomationModel } from '@models/automation/automation.model';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { FormatDatePipe } from '@app/pipes';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'tr-automation-card',
  imports: [NgIcon, FormatDatePipe, NgStyle, RouterLink],
  templateUrl: './automation-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationCardComponent {
  automation = input.required<AutomationModel>();
}
