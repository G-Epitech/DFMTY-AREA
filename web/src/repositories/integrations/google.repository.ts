import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  GoogleCodeDTORequest,
  GoogleCodeDTOResponse,
  GoogleConfigurationDTO,
} from '@repositories/integrations/dto/google';
import { GOOGLE_STATE_CODE_KEY } from '@common/constants';

@Injectable({
  providedIn: 'root',
})
export class GoogleRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getGoogleConfiguration(): Observable<GoogleConfigurationDTO> {
    const url = `${this.baseUrl}/auth/oauth2/google/configuration`;
    return this.#httpClient.get<GoogleConfigurationDTO>(url);
  }

  sendCode(dto: GoogleCodeDTORequest): Observable<GoogleCodeDTOResponse> {
    const url = `${this.baseUrl}/auth/oauth2/google/`;
    return this.#httpClient.post<GoogleCodeDTOResponse>(url, dto);
  }

  storeStateCode(state: string): void {
    localStorage.setItem(GOOGLE_STATE_CODE_KEY, state);
  }

  getStateCode(): string | null {
    return localStorage.getItem(GOOGLE_STATE_CODE_KEY);
  }

  removeStateCode(): void {
    localStorage.removeItem(GOOGLE_STATE_CODE_KEY);
  }
}
