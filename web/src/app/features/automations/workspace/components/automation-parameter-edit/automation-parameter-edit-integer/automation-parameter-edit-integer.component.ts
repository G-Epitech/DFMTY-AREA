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

@Component({
  selector: 'tr-automation-parameter-edit-integer',
  imports: [],
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
}
