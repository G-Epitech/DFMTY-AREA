import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationDiscordProps } from '@models/integration';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrBadgeDirective } from '@triggo-ui/badge';

@Component({
  selector: 'tr-integration-linked-discord',
  imports: [NgOptimizedImage, TrButtonDirective, TrBadgeDirective],
  templateUrl: './integration-linked-discord.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedDiscordComponent {
  props = input.required<IntegrationDiscordProps>();
}
