import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
  signal,
} from '@angular/core';
import { AvailableIntegrationType } from '@common/types';
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
} from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { AutomationStepSelectionButtonComponent } from '@features/automations/workspace/components/edit-sheets/automation-step-selection-button/automation-step-selection-button.component';

@Component({
  selector: 'tr-trigger-selection',
  imports: [AutomationStepSelectionButtonComponent],
  templateUrl: './trigger-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class TriggerSelectionComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null | undefined = null;

  integration = input.required<AvailableIntegrationType>();

  availableTriggers = signal<AutomationSchemaTrigger[]>([]);

  triggerSelected = output<AutomationSchemaTrigger>();

  constructor() {
    effect(() => {
      this.schema = this.#schemaStore.getSchema();
      if (this.schema) {
        this.availableTriggers.set(
          this.schema.getAvailableTriggers(this.integration().name)
        );
      }
    });
  }

  selectTrigger(trigger: AutomationSchemaTrigger) {
    this.triggerSelected.emit(trigger);
  }
}
