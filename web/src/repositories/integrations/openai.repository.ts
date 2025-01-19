import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  OpenaiLinkRequestDTO,
  OpenaiModelDTO,
} from '@repositories/integrations/dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OpenaiRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  link(dto: OpenaiLinkRequestDTO): Observable<void> {
    const url = `${this.baseUrl}/integrations/openai`;
    return this.#httpClient.post<void>(url, dto);
  }

  getModels(integrationId: string): Observable<OpenaiModelDTO[]> {
    const url = `${this.baseUrl}/integrations/${integrationId}/openai/models`;
    return this.#httpClient.get<OpenaiModelDTO[]>(url);
  }
}
