import { inject, Injectable } from '@angular/core';
import {
  DiscordRepository,
  IntegrationsRepository,
} from '@repositories/integrations';
import { map, Observable } from 'rxjs';
import { DiscordGuildModel, IntegrationModel } from '@models/integration';

@Injectable({
  providedIn: 'root',
})
export class IntegrationsMediator {
  readonly #integrationsRepository = inject(IntegrationsRepository);
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

  getById(id: string): Observable<IntegrationModel> {
    return this.#integrationsRepository.getById(id).pipe(
      map(dto => {
        return new IntegrationModel(
          dto.id,
          dto.ownerId,
          dto.isValid,
          dto.type,
          dto.properties
        );
      })
    );
  }
}
