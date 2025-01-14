import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  NotionLinkRequestDTO,
  NotionUriResponseDTO,
} from '@repositories/integrations/dto';

@Injectable({
  providedIn: 'root',
})
export class NotionRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getUri(): Observable<NotionUriResponseDTO> {
    const url = `${this.baseUrl}/integrations/notion/uri`;
    return this.#httpClient.post<NotionUriResponseDTO>(url, {});
  }

  link(dto: NotionLinkRequestDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/notion`;
    return this.#httpClient.post<void>(url, dto);
  }
}
