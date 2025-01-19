import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { GmailUriResponseDTO } from '@repositories/integrations/dto/gmail/gmail-uri.dto';
import { GmailRequestDTO } from '@repositories/integrations/dto/gmail/gmail.dto';

@Injectable({
  providedIn: 'root',
})
export class GmailRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getUri(): Observable<string> {
    const url = `${this.baseUrl}/integrations/gmail/uri`;
    const response = this.#httpClient.post<GmailUriResponseDTO>(url, {});
    return response.pipe(map(res => res.uri));
  }

  link(dto: GmailRequestDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/gmail`;
    return this.#httpClient.post<void>(url, dto);
  }
}
