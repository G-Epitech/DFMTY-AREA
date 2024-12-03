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
