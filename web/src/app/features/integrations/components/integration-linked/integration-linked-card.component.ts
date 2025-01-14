import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  signal,
} from '@angular/core';
import { IntegrationModel, IntegrationTypeEnum } from '@models/integration';
import { IntegrationLinkedDiscordComponent } from '@features/integrations/discord/integration-linked-discord/integration-linked-discord.component';
import { SchemaStore } from '@app/store/schema-store';
import { IntegrationLinkedNotionComponent } from '@features/integrations/notion/integration-linked-notion/integration-linked-notion.component';

@Component({
  selector: 'tr-integration-linked-card',
  imports: [
    IntegrationLinkedDiscordComponent,
    IntegrationLinkedNotionComponent,
  ],
  templateUrl: './integration-linked-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedCardComponent {
  readonly #schemaStore = inject(SchemaStore);

  integration = input.required<IntegrationModel>();
  schema = this.#schemaStore.getSchema();
  iconUri = signal<string>('');
  color = signal<string>('');

  constructor() {
    effect(() => {
      if (this.schema && this.integration) {
        const integrationName = this.integration().type.toString();
        this.iconUri.set(this.schema.getIntegrationIconUri(integrationName));
        this.color.set(this.schema.getIntegrationColor(integrationName));
      }
    });
  }

  protected readonly IntegrationTypeEnum = IntegrationTypeEnum;
}
