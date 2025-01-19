import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LeagueOfLegendsLinkDTO } from '@repositories/integrations/dto/league-of-legends';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LeagueOfLegendsRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  link(dto: LeagueOfLegendsLinkDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/lol`;
    return this.#httpClient.post<void>(url, dto);
  }
}
