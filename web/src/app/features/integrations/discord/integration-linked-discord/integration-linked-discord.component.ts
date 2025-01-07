import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IntegrationDiscordProps } from '@models/integration';
import { NgOptimizedImage } from '@angular/common';
import { ManageGuildDialogComponent } from '@features/integrations/discord/manage-guild-dialog/manage-guild-dialog.component';

@Component({
  selector: 'tr-integration-linked-discord',
  imports: [NgOptimizedImage, ManageGuildDialogComponent],
  templateUrl: './integration-linked-discord.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedDiscordComponent {
  props = input.required<IntegrationDiscordProps>();
}
