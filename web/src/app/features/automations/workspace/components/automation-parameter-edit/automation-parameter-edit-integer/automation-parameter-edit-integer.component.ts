import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
} from '@angular/core';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterType } from '@models/automation';
import { TrInputDirective } from '@triggo-ui/input';
import { NumericOnlyDirective } from '@app/directives/numeric-only.directive';

@Component({
  selector: 'tr-automation-parameter-edit-integer',
  imports: [TrInputDirective, NumericOnlyDirective],
  templateUrl: './automation-parameter-edit-integer.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditIntegerComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterType;
  valueChange = new EventEmitter<ParameterEditOutput>();

  onValueChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.valueChange.emit({ rawValue: value, displayValue: value });
  }
}
