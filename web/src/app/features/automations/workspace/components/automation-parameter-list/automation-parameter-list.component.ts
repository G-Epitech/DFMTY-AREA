import {
  ChangeDetectionStrategy,
  Component,
  input,
  output,
} from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-automation-parameter-list',
  imports: [PascalToPhrasePipe, NgIcon],
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

  editParameter = output<{ identifier: string; value: string | null }>();
}
