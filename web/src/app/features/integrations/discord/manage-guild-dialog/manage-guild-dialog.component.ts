import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { injectBrnDialogContext } from '@spartan-ng/ui-dialog-brain';
import { AsyncPipe, NgClass, NgOptimizedImage } from '@angular/common';
import { TrDialogImports } from '@triggo-ui/dialog';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { IntegrationsMediator } from '@mediators/integrations.mediator';
import { Observable } from 'rxjs';
import { DiscordGuildModel } from '@models/integration';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';
import { environment } from '@environments/environment';

@Component({
  selector: 'tr-manage-guild-dialog',
  imports: [
    NgOptimizedImage,
    TrDialogImports,
    NgIcon,
    NgClass,
    TrButtonDirective,
    TrInputSearchComponent,
    AsyncPipe,
    TrSkeletonComponent,
  ],
  templateUrl: './manage-guild-dialog.component.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ManageGuildDialogComponent implements OnInit {
  readonly #integrationMediator = inject(IntegrationsMediator);
  readonly #dialogContext = injectBrnDialogContext<{
    integrationId: string;
  }>();

  protected readonly integrationId = this.#dialogContext.integrationId;

  guilds$: Observable<DiscordGuildModel[]> | undefined;

  ngOnInit() {
    this.guilds$ = this.#integrationMediator.getDiscordGuilds(
      this.integrationId
    );
  }

  linkGuild() {
    const clientId = environment.integrationSettingsClientId;
    const uri = `https://discord.com/oauth2/authorize?client_id=${clientId}&permissions=8&integration_type=0&scope=bot`;
    window.open(uri, '_blank');
  }

  linkGuildById(guildId: string) {
    const clientId = environment.integrationSettingsClientId;
    const uri = `https://discord.com/oauth2/authorize?client_id=${clientId}&permissions=8&integration_type=0&scope=bot&guild_id=${guildId}`;
    window.open(uri, '_blank');
  }
}
