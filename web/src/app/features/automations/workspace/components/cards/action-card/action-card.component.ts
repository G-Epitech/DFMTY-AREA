import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  input,
} from '@angular/core';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { ActionModel, AutomationSchemaModel } from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { iconNameFromIdentifier } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-action-card',
  imports: [NgIcon, NgStyle, NgOptimizedImage],
  templateUrl: './action-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ActionCardComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;
  action = input.required<ActionModel | null>();

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
        return iconNameFromIdentifier(iconIdentifier);
      }
    }
    return null;
  });

  iconIntegration = computed(() => {
    const action = this.action();
    if (action && this.schema) {
      return this.schema.getIntegrationIconUri(action.integration);
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

  paramsInvalid = computed(() => {
    for (const param of this.action()?.parameters || []) {
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
