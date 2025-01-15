import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  signal,
} from '@angular/core';
import { IntegrationDiscordProps, IntegrationModel } from '@models/integration';
import { NgOptimizedImage, NgStyle } from '@angular/common';
import { ManageGuildDialogComponent } from '@features/integrations/discord/manage-guild-dialog/manage-guild-dialog.component';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrDialogService } from '@triggo-ui/dialog';

@Component({
  selector: 'tr-integration-linked-discord',
  imports: [NgOptimizedImage, TrButtonDirective, NgStyle],
  templateUrl: './integration-linked-discord.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedDiscordComponent {
  readonly #dialogService = inject(TrDialogService);

  integration = input.required<IntegrationModel>();
  iconUri = input.required<string | null>();
  color = input.required<string | null>();

  discordProps = signal<IntegrationDiscordProps | null>(null);

  constructor() {
    effect(() => {
      if (this.integration()) {
        this.discordProps.set(
          this.integration().props as IntegrationDiscordProps
        );
      }
    });
  }

  openModal() {
    this.#dialogService.open(ManageGuildDialogComponent, {
      context: {
        integrationId: this.integration().id,
      },
      contentClass: 'min-w-[800px] min-h-[600px]',
    });
  }
}
