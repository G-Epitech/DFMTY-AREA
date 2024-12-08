import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UsersGetResponseDTO } from '@repositories/users/dto';
import { map } from 'rxjs';
import { UserModel } from '@models/user.model';
import { DiscordUriResponseDTO } from '@repositories/integrations/dto/discord';
import { DiscordRequestDTO } from '@repositories/integrations/dto/discord/discord.dto';

@Injectable({
  providedIn: 'root',
})
export class DiscordRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getUri() {
    const url = `${this.baseUrl}/integrations/discord/uri`;
    const response = this.#httpClient.post<DiscordUriResponseDTO>(url, {});
    return response.pipe(
      map(
        res => res.uri
      )
    );
  }

  link(dto: DiscordRequestDTO) {
    const url = `${this.baseUrl}/integrations/discord`;
    const response = this.#httpClient.post<null>(url, dto);
    return response.pipe(
      map(
        res => true
      )
    );
  }
}
