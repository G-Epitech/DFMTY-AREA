import { inject, Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { GoogleAuthConfigurationModel } from '@models/google-auth-configuration.model';
import { GoogleRepository } from '@repositories/integrations/google.repository';
import { TokensModel } from '@models/tokens.model';
import { TokenRepository } from '@repositories/auth';

@Injectable({
  providedIn: 'root',
})
export class GoogleMediator {
  readonly #googleRepository = inject(GoogleRepository);
  readonly #tokenRepository = inject(TokenRepository);

  getGoogleConfiguration(): Observable<GoogleAuthConfigurationModel> {
    return this.#googleRepository.getGoogleConfiguration().pipe(
      map(res => {
        const provider = res.clientIds.find(
          ({ provider }) => provider === 'Web'
        );
        if (!provider) {
          throw new Error('Google provider not found');
        }
        return new GoogleAuthConfigurationModel(
          res.scopes,
          provider.clientId,
          res.endpoint
        );
      })
    );
  }

  sendCode(code: string): Observable<TokensModel> {
    return this.#googleRepository.sendCode({ code }).pipe(
      map(res => new TokensModel(res.accessToken, res.refreshToken)),
      tap(tokens => {
        this.#tokenRepository.storeTokens(tokens);
      })
    );
  }

  generateRandomString(length: number): string {
    const array = new Uint8Array(length);
    crypto.getRandomValues(array);
    return Array.from(array, byte => byte.toString(16).padStart(2, '0')).join(
      ''
    );
  }

  storeStateCode(state: string): void {
    this.#googleRepository.storeStateCode(state);
  }

  getStateCode(): string | null {
    return this.#googleRepository.getStateCode();
  }

  removeStateCode(): void {
    this.#googleRepository.removeStateCode();
  }
}
