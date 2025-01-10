import { inject, Inject, Injectable } from '@angular/core';
import {
  AuthLoginRequestDTO,
  AuthLoginResponseDTO,
  AuthRegisterRequestDTO,
  AuthRegisterResponseDTO,
} from './dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  register(dto: AuthRegisterRequestDTO): Observable<AuthRegisterResponseDTO> {
    const url = `${this.baseUrl}/auth/register`;
    return this.#httpClient.post<AuthRegisterResponseDTO>(url, dto);
  }

  login(dto: AuthLoginRequestDTO): Observable<AuthLoginResponseDTO> {
    const url = `${this.baseUrl}/auth/login`;
    return this.#httpClient.post<AuthLoginResponseDTO>(url, dto);
  }
}
