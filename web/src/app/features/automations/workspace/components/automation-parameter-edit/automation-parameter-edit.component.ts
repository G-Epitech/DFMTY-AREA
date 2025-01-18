import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
  signal,
  ViewContainerRef,
} from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import {
  ActionParameter,
  AutomationParameterValueType,
  TriggerParameter,
} from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TrTabsImports } from '@triggo-ui/tabs';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { NgClass } from '@angular/common';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

@Component({
  standalone: true,
  selector: 'tr-automation-parameter-edit',
  imports: [PascalToPhrasePipe, NgIcon, TrTabsImports, NgClass],
  templateUrl: './automation-parameter-edit.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [],
})
export class AutomationParameterEditComponent {
  private readonly editService = inject(AutomationParameterEditService);
  private readonly viewContainerRef = inject(ViewContainerRef);
  readonly #editService = inject(AutomationParameterEditService);

  readonly integrationId = input.required<string>();
  readonly parameter = input.required<ActionParameter | TriggerParameter>();
  readonly parameterDescription = input.required<string | undefined>();
  readonly parameterType = input.required<AutomationParameterValueType>();
  readonly displayPrevious = input<boolean>(false);

  readonly activeTab = signal<string>('raw');

  readonly parameterChangeEmitter = output<ParameterEditOutput>();

  constructor() {
    effect(() => {
      this.createDynamicComponentIfNeeded();
    });
  }

  private createDynamicComponentIfNeeded(): void {
    this.viewContainerRef.clear();
    if (!this.shouldCreateDynamicComponent()) {
      return;
    }
    const component = this.editService.getParameterEditComponents(
      this.parameter().identifier,
      this.parameterType()
    );
    const componentRef =
      this.viewContainerRef.createComponent<ParameterEditDynamicComponent>(
        component
      );
    if (!componentRef.instance) {
      return;
    }
    this.setupDynamicComponent(componentRef.instance);
  }

  private shouldCreateDynamicComponent(): boolean {
    return (
      this.activeTab() === 'raw' && !!this.parameter() && !!this.parameterType()
    );
  }

  private setupDynamicComponent(instance: ParameterEditDynamicComponent) {
    instance.parameter = this.parameter();
    instance.parameterType = this.parameterType();
    instance.integrationId = this.integrationId();

    if (instance.valueChange) {
      instance.valueChange.subscribe(value => {
        this.#editService.currentParameters.update(parameters => {
          const index = parameters.findIndex(
            ({ identifier }) => identifier === this.parameter().identifier
          );
          if (index === -1) {
            parameters.push({
              type: this.parameter().type,
              identifier: this.parameter().identifier,
              value: value.rawValue,
            });
          } else {
            parameters[index].value = value.rawValue;
          }
          return parameters;
        });
        this.parameterChangeEmitter.emit(value);
      });
    }
  }

  onTabChange(tab: string): void {
    this.activeTab.set(tab);
  }
}
