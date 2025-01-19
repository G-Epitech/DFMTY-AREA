import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { DeepAutomationFact } from '../automation-parameter-edit-previous-facts.types';
import { PascalToPhrasePipe } from '@app/pipes';
import { NgClass } from '@angular/common';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-previous-fact-button',
  imports: [TrButtonDirective, PascalToPhrasePipe, NgClass, NgIcon],
  templateUrl: './previous-fact-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class PreviousFactButtonComponent {
  fact = input.required<DeepAutomationFact>();
  checked = input<boolean>(false);
}
