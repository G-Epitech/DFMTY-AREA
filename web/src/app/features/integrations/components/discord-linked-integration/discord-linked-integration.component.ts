import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationDiscordProps } from '@models/integration';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrBadgeDirective } from '@triggo-ui/badge';

@Component({
  selector: 'tr-discord-linked-integration',
  imports: [NgOptimizedImage, TrButtonDirective, TrBadgeDirective],
  templateUrl: './discord-linked-integration.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DiscordLinkedIntegrationComponent {
  props = input.required<IntegrationDiscordProps>();
}
