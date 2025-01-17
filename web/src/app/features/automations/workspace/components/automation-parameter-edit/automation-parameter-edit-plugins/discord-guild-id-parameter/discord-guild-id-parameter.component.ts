import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EventEmitter,
  inject,
} from '@angular/core';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterType } from '@models/automation';
import { IntegrationsMediator } from '@mediators/integrations';
import { map, Observable } from 'rxjs';
import { DiscordGuildModel } from '@models/integration';
import { AsyncPipe, NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { filter } from 'rxjs/operators';

@Component({
  standalone: true,
  selector: 'tr-discord-guild-id-parameter',
  imports: [AsyncPipe, NgOptimizedImage, TrButtonDirective],
  templateUrl: './discord-guild-id-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DiscordGuildIdParameterComponent
  implements ParameterEditDynamicComponent
{
  readonly #integrationsMediator = inject(IntegrationsMediator);

  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  discordGuilds$: Observable<DiscordGuildModel[]> | undefined;

  constructor() {
    effect(() => {
      if (this.integrationId) {
        this.discordGuilds$ = this.#integrationsMediator
          .getDiscordGuilds(this.integrationId)
          .pipe(map(guilds => guilds.filter(guild => guild.linked)));
      }
    });
  }

  selectGuild(guild: DiscordGuildModel): void {
    this.valueChange.emit({ rawValue: guild.id, displayValue: guild.name });
  }
}
