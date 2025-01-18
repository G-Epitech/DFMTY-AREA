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
import { TriggerSelectionButtonComponent } from '@features/automations/workspace/components/edit-sheets/trigger-selection-button/trigger-selection-button.component';

@Component({
  selector: 'tr-trigger-selection',
  imports: [TriggerSelectionButtonComponent],
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
