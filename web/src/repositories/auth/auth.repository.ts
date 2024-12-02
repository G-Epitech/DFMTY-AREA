import { inject, Inject, Injectable } from '@angular/core';
import {
  AuthLoginRequestDTO,
  AuthLoginResponseDTO,
  AuthRegisterRequestDTO,
  AuthRegisterResponseDTO,
} from './dto';
import { map, Observable, of } from 'rxjs';
import { TokensModel } from '@models/tokens.model';
import { AuthUserModel } from '@models/auth-user.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  storeTokens(tokens: TokensModel): void {
    if (tokens.accessToken) {
      localStorage.setItem('accessToken', tokens.accessToken);
    }
    if (tokens.refreshToken) {
      localStorage.setItem('refreshToken', tokens.refreshToken);
    }
  }

  getTokens(): TokensModel {
    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');

    return new TokensModel(accessToken, refreshToken);
  }

  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  clearTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  register(dto: AuthRegisterRequestDTO): Observable<TokensModel> {
    const url = `${this.baseUrl}/auth/register`;
    const response = this.#httpClient.post<AuthRegisterResponseDTO>(url, dto);
    return response.pipe(
      map(res => {
        return new TokensModel(res.accessToken, res.refreshToken);
      })
    );
  }

  login(dto: AuthLoginRequestDTO): Observable<TokensModel> {
    const url = `${this.baseUrl}/auth/login`;
    const response = this.#httpClient.post<AuthLoginResponseDTO>(url, dto);
    return response.pipe(
      map(res => {
        return new TokensModel(res.accessToken, res.refreshToken);
      })
    );
  }

  me(): Observable<AuthUserModel> {
    return of(new AuthUserModel('1', 'email', 'first-name', 'last-name'));
  }
}
