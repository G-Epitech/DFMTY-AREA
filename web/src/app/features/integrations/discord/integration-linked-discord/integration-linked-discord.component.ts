import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
} from '@angular/core';
import { IntegrationDiscordProps, IntegrationModel } from '@models/integration';
import { NgOptimizedImage } from '@angular/common';
import { ManageGuildDialogComponent } from '@features/integrations/discord/manage-guild-dialog/manage-guild-dialog.component';
import { BrnDialogTriggerDirective } from '@spartan-ng/ui-dialog-brain';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrDialogService } from '@triggo-ui/dialog';

@Component({
  selector: 'tr-integration-linked-discord',
  imports: [
    NgOptimizedImage,
    ManageGuildDialogComponent,
    BrnDialogTriggerDirective,
    TrButtonDirective,
  ],
  templateUrl: './integration-linked-discord.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationLinkedDiscordComponent {
  readonly #dialogService = inject(TrDialogService);

  integration = input.required<IntegrationModel>();

  openModal() {
    this.#dialogService.open(ManageGuildDialogComponent, {
      context: {
        integrationId: this.integration().id,
      },
      contentClass: 'min-w-[800px] min-h-[600px]',
    });
  }
}
