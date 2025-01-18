import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
  signal,
} from '@angular/core';
import { SchemaStore } from '@app/store/schema-store';
import {
  AutomationSchemaAction,
  AutomationSchemaModel,
} from '@models/automation';
import { AvailableIntegrationType } from '@common/types';
import {
  AutomationStepSelectionButtonComponent
} from '@features/automations/workspace/components/edit-sheets/automation-step-selection-button/automation-step-selection-button.component';


@Component({
  selector: 'tr-action-selection',
  imports: [AutomationStepSelectionButtonComponent],
  templateUrl: './action-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ActionSelectionComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null | undefined = null;

  integration = input.required<AvailableIntegrationType>();

  availableActions = signal<AutomationSchemaAction[]>([]);

  actionSelected = output<AutomationSchemaAction>();

  constructor() {
    effect(() => {
      this.schema = this.#schemaStore.getSchema();
      if (this.schema) {
        this.availableActions.set(
          this.schema.getAvailableActions(this.integration().name)
        );
      }
    });
  }

  selectAction(trigger: AutomationSchemaAction) {
    this.actionSelected.emit(trigger);
  }
}
