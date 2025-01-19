import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  OnInit,
} from '@angular/core';
import { AutomationParameterValueType } from '@models/automation';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { OpenaiMediator } from '@mediators/integrations';
import { Observable } from 'rxjs';
import { OpenaiModel } from '@models/integration';
import { AsyncPipe, NgClass } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-openai-model-parameter',
  imports: [AsyncPipe, NgIcon, TrButtonDirective, NgClass],
  templateUrl: './openai-model-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OpenaiModelParameterComponent
  implements ParameterEditDynamicComponent, OnInit
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  readonly #openaiMediator = inject(OpenaiMediator);

  models$: Observable<OpenaiModel[]> | undefined;

  ngOnInit() {
    if (this.integrationId) {
      this.models$ = this.#openaiMediator.getModels(this.integrationId);
    }
  }

  selectModel(model: OpenaiModel) {
    this.valueChange.emit({ rawValue: model.id });
  }

  isModelSelected(model: OpenaiModel): boolean {
    return model.id === this.parameter.value;
  }
}
