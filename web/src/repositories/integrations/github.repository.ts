import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GithubUriResponseDTO } from '@repositories/integrations/dto/github';
import { DiscordRequestDTO } from '@repositories/integrations/dto/discord/discord.dto';

@Injectable({
  providedIn: 'root',
})
export class GithubRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getUri(): Observable<GithubUriResponseDTO> {
    const url = `${this.baseUrl}/integrations/github/uri`;
    return this.#httpClient.post<GithubUriResponseDTO>(url, {});
  }

  link(dto: DiscordRequestDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/github`;
    return this.#httpClient.post<void>(url, dto);
  }
}
