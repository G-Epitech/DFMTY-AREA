import { inject, Injectable } from '@angular/core';
import { AuthRepository, TokenRepository } from '@repositories/auth';
import { map, Observable, tap } from 'rxjs';
import { TokensModel } from '@models/tokens.model';
import { AppRouter } from '@app/app.router';
import { GoogleAuthConfigurationModel } from '@models/google-auth-configuration.model';

@Injectable({
  providedIn: 'root',
})
export class AuthMediator {
  readonly #authRepository = inject(AuthRepository);
  readonly #appRouter = inject(AppRouter);
  readonly #tokenRepository = inject(TokenRepository);

  register(
    email: string,
    password: string,
    firstName: string,
    lastName: string
  ): Observable<TokensModel> {
    return this.#authRepository
      .register({
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName,
      })
      .pipe(
        map(res => new TokensModel(res.accessToken, res.refreshToken)),
        tap(tokens => this.#tokenRepository.storeTokens(tokens))
      );
  }

  login(email: string, password: string): Observable<TokensModel> {
    return this.#authRepository
      .login({
        email: email,
        password: password,
      })
      .pipe(
        map(res => new TokensModel(res.accessToken, res.refreshToken)),
        tap(tokens => this.#tokenRepository.storeTokens(tokens))
      );
  }

  logout(): void {
    this.#tokenRepository.clearTokens();
    void this.#appRouter.redirectToLogin();
  }
}
