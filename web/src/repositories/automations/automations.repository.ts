import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { AutomationDTO, AutomationManifestDTO } from '@repositories/dto';

@Injectable({
  providedIn: 'root',
})
export class AutomationsRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getById(id: string): Observable<AutomationDTO> {
    const url = `${this.baseUrl}/automations/${id}`;
    return this.#httpClient.get<AutomationDTO>(url);
  }

  post(): Observable<string> {
    return of('id');
  }

  getManifest(): Observable<AutomationManifestDTO> {
    const url = `${this.baseUrl}/automations/manifest`;
    return this.#httpClient.get<AutomationManifestDTO>(url);
  }
}
