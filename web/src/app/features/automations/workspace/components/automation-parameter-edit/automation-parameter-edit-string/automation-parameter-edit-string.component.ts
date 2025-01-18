import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
} from '@angular/core';
import { TrInputDirective } from '@triggo-ui/input';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterType } from '@models/automation';

@Component({
  selector: 'tr-automation-parameter-edit-string',
  imports: [TrInputDirective],
  templateUrl: './automation-parameter-edit-string.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditStringComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterType;
  valueChange = new EventEmitter<ParameterEditOutput>();

  onValueChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.valueChange.emit({ rawValue: value });
  }
}
