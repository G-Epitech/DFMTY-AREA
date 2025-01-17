import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-automation-parameter-list',
  imports: [PascalToPhrasePipe, NgIcon, TrButtonDirective],
  templateUrl: './automation-parameter-list.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterListComponent {
  parameters = input.required<
    {
      identifier: string;
      value: string | null;
    }[]
  >();
}
