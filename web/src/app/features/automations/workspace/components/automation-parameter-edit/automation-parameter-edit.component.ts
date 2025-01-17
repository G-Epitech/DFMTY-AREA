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
import { AutomationParameterType } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TrTabsImports } from '@triggo-ui/tabs';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { NgClass } from '@angular/common';
import { ParameterEditOutput } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';

interface DynamicComponent {
  parameter: { identifier: string; value: string | null };
  parameterType: AutomationParameterType;
  valueChange?: {
    subscribe: (callback: (value: ParameterEditOutput) => void) => void;
  };
}

@Component({
  standalone: true,
  selector: 'tr-automation-parameter-edit',
  imports: [PascalToPhrasePipe, NgIcon, TrTabsImports, NgClass],
  templateUrl: './automation-parameter-edit.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [AutomationParameterEditService],
})
export class AutomationParameterEditComponent {
  private readonly editService = inject(AutomationParameterEditService);
  private readonly viewContainerRef = inject(ViewContainerRef);

  readonly automationStepIdentifier = input.required<string>();
  readonly parameter = input.required<{
    identifier: string;
    value: string | null;
  }>();
  readonly parameterType = input.required<AutomationParameterType>();
  readonly displayPrevious = input<boolean>(false);

  readonly activeTab = signal<string>('raw');

  readonly parameterChange = signal<ParameterEditOutput>({
    displayValue: '',
    rawValue: '',
  });
  readonly parameterChangeEmitter = output<ParameterEditOutput>();

  constructor() {
    effect(() => {
      if (this.parameter()) {
        this.parameterChange.set({
          displayValue: this.parameter()?.value || '',
          rawValue: this.parameter()?.value || '',
        });
      }
    });

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
      this.automationStepIdentifier(),
      this.parameterType()
    );
    const componentRef =
      this.viewContainerRef.createComponent<DynamicComponent>(component);
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

  private setupDynamicComponent(instance: DynamicComponent) {
    instance.parameter = this.parameter();
    instance.parameterType = this.parameterType();

    if (instance.valueChange) {
      instance.valueChange.subscribe(value => {
        this.parameterChange.set(value);
        this.parameterChangeEmitter.emit(value);
      });
    }
  }

  onTabChange(tab: string): void {
    this.activeTab.set(tab);
  }
}
