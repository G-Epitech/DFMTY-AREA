import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  input,
} from '@angular/core';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { ActionShortModel, AutomationSchemaModel } from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-action-card',
  imports: [NgIcon, NgStyle],
  templateUrl: './action-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ActionCardComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;
  action = input.required<ActionShortModel>();

  color = computed(() => {
    const action = this.action();
    if (action && this.schema) {
      return this.schema.getIntegrationColor(action.integration);
    }
    return null;
  });

  icon = computed(() => {
    const action = this.action();
    if (action && this.schema) {
      const iconIdentifier = this.schema.getActionIcon(
        action.integration,
        action.nameIdentifier
      );
      if (iconIdentifier) {
        return iconName(iconIdentifier);
      }
    }
    return null;
  });

  name = computed(() => {
    const action = this.action();
    if (action && this.schema) {
      return this.schema.getActionName(
        action.integration,
        action.nameIdentifier
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
