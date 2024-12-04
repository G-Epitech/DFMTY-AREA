import { inject, Injectable } from '@angular/core';
import { TokenRepository } from '@repositories/auth';

@Injectable({
  providedIn: 'root',
})
export class TokenMediator {
  readonly #tokenRepository = inject(TokenRepository);

  getTokens() {
    return this.#tokenRepository.getTokens();
  }

  getAccessToken() {
    return this.#tokenRepository.getAccessToken();
  }

  getRefreshToken() {
    return this.#tokenRepository.getRefreshToken();
  }

  accessTokenIsValid(): boolean {
    const tokens = this.getTokens();
    return tokens.accessToken !== null && tokens.accessToken !== undefined;
  }
}
