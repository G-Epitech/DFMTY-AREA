import { inject, Injectable } from '@angular/core';
import {
  DiscordRepository,
  IntegrationsRepository,
} from '@repositories/integrations';
import { map, Observable } from 'rxjs';
import { DiscordGuildModel, IntegrationModel } from '@models/integration';
import { DiscordChannelModel } from '@models/integration/discord/discord-channel.model';

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

  getDiscordChannels(
    integrationId: string,
    guildId: string
  ): Observable<DiscordChannelModel[]> {
    return this.#discordRepository.getChannels(integrationId, guildId).pipe(
      map(dto => {
        return dto.map(channel => {
          return new DiscordChannelModel(channel.id, channel.name);
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
