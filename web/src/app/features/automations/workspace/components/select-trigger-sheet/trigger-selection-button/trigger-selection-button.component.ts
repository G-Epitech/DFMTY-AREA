import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  signal,
} from '@angular/core';
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
} from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { SelectTriggerSheetService } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.service';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { iconNameFromIdentifier } from '@utils/icon';

@Component({
  selector: 'tr-trigger-selection-button',
  imports: [NgIcon, NgStyle],
  templateUrl: './trigger-selection-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class TriggerSelectionButtonComponent {
  readonly #schemaStore = inject(SchemaStore);
  readonly #service = inject(SelectTriggerSheetService);

  schema: AutomationSchemaModel | null | undefined = null;

  trigger = input.required<AutomationSchemaTrigger>();

  color = signal<string | null>('');
  icon = signal<string>('');

  constructor() {
    effect(() => {
      this.schema = this.#schemaStore.getSchema();
      if (this.schema) {
        this.color.set(
          this.schema.getIntegrationColor(
            this.#service.baseState().selectedIntegration!.name
          )
        );
      }
    });
    effect(() => {
      this.icon.set(iconNameFromIdentifier(this.trigger().icon));
    });
  }
}
