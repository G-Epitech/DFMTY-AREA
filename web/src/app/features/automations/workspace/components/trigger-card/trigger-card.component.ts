import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  input,
  signal,
} from '@angular/core';
import { NgClass, NgStyle } from '@angular/common';
import { AutomationSchemaModel, TriggerShortModel } from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-trigger-card',
  templateUrl: './trigger-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [NgStyle, NgIcon],
})
export class TriggerCardComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;

  trigger = input.required<TriggerShortModel | null>();

  color = computed(() => {
    const trigger = this.trigger();
    if (trigger && this.schema) {
      return this.schema.getIntegrationColor(trigger.integration);
    }
    return null;
  });

  icon = computed(() => {
    const trigger = this.trigger();
    if (trigger && this.schema) {
      const iconIdentifier = this.schema.getTriggerIcon(
        trigger.integration,
        trigger.nameIdentifier
      );
      if (iconIdentifier) {
        return iconName(iconIdentifier);
      }
    }
    return null;
  });

  name = computed(() => {
    const trigger = this.trigger();
    if (trigger && this.schema) {
      return this.schema.getTriggerName(
        trigger.integration,
        trigger.nameIdentifier
      );
    }
    return null;
  });

  constructor() {
    effect(() => {
      const schema = this.#schemaStore.getSchema();
      if (schema) {
        this.schema = schema;
      }
    });
  }
}
