import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
} from '@angular/core';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterValueType } from '@models/automation';
import { TrInputDirective } from '@triggo-ui/input';
import { FloatOnlyDirective } from '@app/directives/float-only.directive';

@Component({
  selector: 'tr-automation-parameter-edit-float',
  imports: [TrInputDirective, FloatOnlyDirective],
  templateUrl: './automation-parameter-edit-float.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditFloatComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();

  onValueChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.valueChange.emit({ rawValue: value });
  }
}
