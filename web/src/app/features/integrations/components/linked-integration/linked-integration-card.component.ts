import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationModel, IntegrationTypeEnum } from '@models/integration';
import { DiscordLinkedIntegrationComponent } from '@features/integrations/components/discord-linked-integration/discord-linked-integration.component';

@Component({
  selector: 'tr-linked-integration-card',
  imports: [DiscordLinkedIntegrationComponent],
  templateUrl: './linked-integration-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LinkedIntegrationCardComponent {
  integration = input.required<IntegrationModel>();

  protected readonly IntegrationTypeEnum = IntegrationTypeEnum;
}
