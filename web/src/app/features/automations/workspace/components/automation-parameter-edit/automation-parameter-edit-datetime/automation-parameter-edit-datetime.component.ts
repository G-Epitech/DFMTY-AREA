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

@Component({
  selector: 'tr-automation-parameter-edit-datetime',
  imports: [TrInputDirective],
  templateUrl: './automation-parameter-edit-datetime.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditDatetimeComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterType;
  valueChange = new EventEmitter<ParameterEditOutput>();

  onValueChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    const isoString = new Date(value).toISOString();
    this.valueChange.emit({ rawValue: isoString });
  }
}
