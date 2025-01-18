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
import { AutomationParameterEditPreviousFactsComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-previous-facts/automation-parameter-edit-previous-facts.component';
import { DeepAutomationFact } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-previous-facts/automation-parameter-edit-previous-facts.types';
import { AutomationParameterFormatType } from '@models/automation/automation-parameter-format-type';

@Component({
  standalone: true,
  selector: 'tr-automation-parameter-edit',
  imports: [
    PascalToPhrasePipe,
    NgIcon,
    TrTabsImports,
    NgClass,
    AutomationParameterEditPreviousFactsComponent,
  ],
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

  readonly activeTab = signal<string>('Raw');

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
      this.activeTab() === (AutomationParameterFormatType.RAW as string) &&
      !!this.parameter() &&
      !!this.parameterType()
    );
  }

  private setupDynamicComponent(instance: ParameterEditDynamicComponent) {
    instance.parameter = this.parameter();
    instance.parameterType = this.parameterType();
    instance.integrationId = this.integrationId();

    if (instance.valueChange) {
      instance.valueChange.subscribe(value =>
        this._changeParamer(value.rawValue, AutomationParameterFormatType.RAW)
      );
    }
  }

  onFactSelected(fact: DeepAutomationFact) {
    this._changeParamer(fact.identifier, AutomationParameterFormatType.VAR);
  }

  onTabChange(tab: string): void {
    this.activeTab.set(tab);
  }

  _changeParamer(value: string, type: AutomationParameterFormatType) {
    this.#editService.currentParameters.update(parameters => {
      const index = parameters.findIndex(
        ({ identifier }) => identifier === this.parameter().identifier
      );
      if (index === -1) {
        parameters.push({
          type: type,
          identifier: this.parameter().identifier,
          value: value,
        });
      } else {
        parameters[index].value = value;
        parameters[index].type = type;
      }
      return parameters;
    });
    this.parameterChangeEmitter.emit({ rawValue: value });
  }
}
