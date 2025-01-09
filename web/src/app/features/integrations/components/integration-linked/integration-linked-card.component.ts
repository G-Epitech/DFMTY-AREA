import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationModel, IntegrationTypeEnum } from '@models/integration';
import { IntegrationLinkedDiscordComponent } from '@features/integrations/discord/integration-linked-discord/integration-linked-discord.component';

@Component({
  selector: 'tr-integration-linked-card',
  imports: [IntegrationLinkedDiscordComponent],
  templateUrl: './integration-linked-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedCardComponent {
  integration = input.required<IntegrationModel>();

  protected readonly IntegrationTypeEnum = IntegrationTypeEnum;
}
