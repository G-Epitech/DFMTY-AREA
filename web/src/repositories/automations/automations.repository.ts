import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  AutomationDTO,
  AutomationManifestDTO,
} from '@repositories/automations/dto';

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
    const url = `${this.baseUrl}/automations`;
    return this.#httpClient.post<string>(url, {});
  }

  getManifest(): Observable<AutomationManifestDTO> {
    const url = `${this.baseUrl}/automations/manifest`;
    return this.#httpClient.get<AutomationManifestDTO>(url);
  }
}
