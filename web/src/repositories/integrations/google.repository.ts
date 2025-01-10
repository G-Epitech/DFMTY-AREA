import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGoogleConfigurationDTO } from '@repositories/auth';

@Injectable({
  providedIn: 'root',
})
export class GoogleRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getGoogleConfiguration(): Observable<AuthGoogleConfigurationDTO> {
    const url = `${this.baseUrl}/auth/oauth2/google/configuration`;
    return this.#httpClient.get<AuthGoogleConfigurationDTO>(url);
  }
}
