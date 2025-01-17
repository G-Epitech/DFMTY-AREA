import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  ViewContainerRef,
} from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import { AutomationParameterType } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TrTabsImports } from '@triggo-ui/tabs';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';

@Component({
  standalone: true,
  selector: 'tr-automation-parameter-edit',
  imports: [PascalToPhrasePipe, NgIcon, TrTabsImports],
  templateUrl: './automation-parameter-edit.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [AutomationParameterEditService],
})
export class AutomationParameterEditComponent {
  readonly #editService = inject(AutomationParameterEditService);
  readonly #viewContainerRef = inject(ViewContainerRef);

  automationStepIdentifier = input.required<string>();
  parameter = input.required<{ identifier: string; value: string | null }>();
  parameterType = input.required<AutomationParameterType>();

  constructor() {
    effect(() => {
      if (this.parameter() && this.parameterType()) {
        this.#viewContainerRef.clear();
        const components = this.#editService.getParameterEditComponents(
          this.automationStepIdentifier(),
          this.parameterType()
        );
        components.forEach(component => {
          const componentRef =
            this.#viewContainerRef.createComponent(component);

          if (componentRef.instance) {
            Object.assign(componentRef.instance, {
              parameter: this.parameter(),
              parameterType: this.parameterType(),
            });
          }
        });
      }
    });
  }
}
