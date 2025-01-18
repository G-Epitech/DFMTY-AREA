import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import { NgIcon } from '@ng-icons/core';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { TrButtonDirective } from '@triggo-ui/button';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

@Component({
  selector: 'tr-automation-parameter-list',
  imports: [PascalToPhrasePipe, NgIcon, TrButtonDirective],
  templateUrl: './automation-parameter-list.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterListComponent {
  readonly parameterEditService = inject(AutomationParameterEditService);

  parameters = input.required<
    {
      type: AutomationParameterFormatType;
      identifier: string;
      value: string | null;
    }[]
  >();

  editParameter = output<{
    type: AutomationParameterFormatType;
    identifier: string;
    value: string | null;
  }>();

  constructor() {
    effect(() => {
      if (this.parameters()) {
        this.parameterEditService.currentParameters.set(this.parameters());
      }
    });
  }
}
