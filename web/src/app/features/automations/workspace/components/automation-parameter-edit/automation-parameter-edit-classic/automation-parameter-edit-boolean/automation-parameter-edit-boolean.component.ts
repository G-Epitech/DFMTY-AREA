import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
} from '@angular/core';
import { TrSwitchImports } from '@triggo-ui/switch';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterValueType } from '@models/automation';

@Component({
  selector: 'tr-automation-parameter-edit-boolean',
  imports: [TrSwitchImports],
  templateUrl: './automation-parameter-edit-boolean.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditBooleanComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();

  onValueChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    const rawValue = value === 'off' ? 'false' : 'true';
    this.valueChange.emit({ rawValue: rawValue });
  }
}
