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
    return this.#googleRepository
      .getGoogleConfiguration()
      .pipe(
        map(
          res =>
            new GoogleAuthConfigurationModel(
              res.scopes,
              res.clientId,
              res.endpoint
            )
        )
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
}
