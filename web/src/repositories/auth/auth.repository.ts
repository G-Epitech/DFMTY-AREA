import {Injectable} from '@angular/core';
import {AuthRegisterRequestDTO, AuthRegisterResponseDTO} from './dto';
import {map, Observable, of} from 'rxjs';
import {TokensModel} from '@models/tokens.model';
import {AuthUserModel} from '@models/auth-user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthRepository {
  readonly #authTokens: AuthRegisterResponseDTO = {
    accessToken: 'access-token',
    refreshToken: 'refresh-token'
  }

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

  register(dto: AuthRegisterRequestDTO): Observable<TokensModel> {
    return of(this.#authTokens).pipe(
      map(response => new TokensModel(
        response.accessToken,
        response.refreshToken
      ))
    );
  }

  me(): Observable<AuthUserModel> {
    return of(new AuthUserModel(
      '1',
      'email',
      'first-name',
      'last-name'
    ));
  }
}
