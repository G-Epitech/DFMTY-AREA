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
import { AsyncPipe, NgClass, NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

@Component({
  standalone: true,
  selector: 'tr-discord-guild-id-parameter',
  imports: [AsyncPipe, NgOptimizedImage, TrButtonDirective, NgClass, NgIcon],
  templateUrl: './discord-guild-id-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DiscordGuildIdParameterComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  readonly #integrationsMediator = inject(IntegrationsMediator);

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
    this.valueChange.emit({ rawValue: guild.id });
  }

  isGuildSelected(guild: DiscordGuildModel): boolean {
    return guild.id === this.parameter.value;
  }
}
