import { inject, Injectable } from '@angular/core';
import { DiscordRepository } from '@repositories/integrations';

@Injectable({
  providedIn: 'root',
})
export class IntegrationsMediator {
  readonly #discordRepository = inject(DiscordRepository);

  get discordRepository(): DiscordRepository {
    return this.#discordRepository;
  }
}
