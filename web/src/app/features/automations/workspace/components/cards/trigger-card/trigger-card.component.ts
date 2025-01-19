import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  input,
} from '@angular/core';
import { NgStyle } from '@angular/common';
import { AutomationSchemaModel, TriggerModel } from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { iconNameFromIdentifier } from '@utils/icon';
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

  trigger = input.required<TriggerModel | null>();

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
        return iconNameFromIdentifier(iconIdentifier);
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

  paramsInvalid = computed(() => {
    for (const param of this.trigger()?.parameters || []) {
      if (!param.value) {
        return true;
      }
    }
    return false;
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
