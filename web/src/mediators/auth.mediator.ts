import { inject, Injectable } from '@angular/core';
import { AuthRepository, TokenRepository } from '@repositories/auth';
import { Observable, tap } from 'rxjs';
import { TokensModel } from '@models/tokens.model';
import { AuthUserModel } from '@models/auth-user.model';
import { AppRouter } from '@app/app.router';

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
        tap({
          next: tokens => this.#tokenRepository.storeTokens(tokens),
          error: error => console.error('Failed to register user', error),
        })
      );
  }

  login(email: string, password: string): Observable<TokensModel> {
    return this.#authRepository
      .login({
        email: email,
        password: password,
      })
      .pipe(
        tap({
          next: tokens => this.#tokenRepository.storeTokens(tokens),
          error: error => console.error('Failed to login user', error),
        })
      );
  }

  me(): Observable<AuthUserModel> {
    return this.#authRepository.me();
  }

  logout(): void {
    this.#tokenRepository.clearTokens();
    void this.#appRouter.redirectToLogin();
  }
}
