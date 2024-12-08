import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { DiscordUriResponseDTO } from '@repositories/integrations/dto/discord';
import { DiscordRequestDTO } from '@repositories/integrations/dto/discord/discord.dto';

@Injectable({
  providedIn: 'root',
})
export class DiscordRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getUri(): Observable<string> {
    const url = `${this.baseUrl}/integrations/discord/uri`;
    const response = this.#httpClient.post<DiscordUriResponseDTO>(url, {});
    return response.pipe(map(res => res.uri));
  }

  link(dto: DiscordRequestDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/discord`;
    return this.#httpClient.post<void>(url, dto);
  }
}
