import { inject, Injectable } from '@angular/core';
import {
  DiscordRepository,
  NotionRepository,
} from '@repositories/integrations';
import { map, Observable } from 'rxjs';
import { DiscordGuildModel } from '@models/integration';

@Injectable({
  providedIn: 'root',
})
export class IntegrationsMediator {
  readonly #discordRepository = inject(DiscordRepository);

  get discordRepository(): DiscordRepository {
    return this.#discordRepository;
  }

  getDiscordGuilds(integrationId: string): Observable<DiscordGuildModel[]> {
    return this.#discordRepository.getGuilds(integrationId).pipe(
      map(dto => {
        return dto.map(guild => {
          return new DiscordGuildModel(
            guild.id,
            guild.name,
            guild.iconUri,
            guild.approximateMemberCount,
            guild.linked
          );
        });
      })
    );
  }
}
