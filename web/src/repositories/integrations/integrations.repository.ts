import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IntegrationDTO } from '@repositories/integrations/dto/integration.dto';

@Injectable({
  providedIn: 'root',
})
export class IntegrationsRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getById(id: string): Observable<IntegrationDTO> {
    const url = `${this.baseUrl}/integrations/${id}`;
    return this.#httpClient.get<IntegrationDTO>(url);
  }
}
