import { inject, Injectable } from '@angular/core';
import { AuthRepository } from '@repositories/auth';
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

  getTokens(): TokensModel {
    return this.#authRepository.getTokens();
  }

  getAccessToken(): string | null {
    return this.#authRepository.getAccessToken();
  }

  getRefreshToken(): string | null {
    return this.#authRepository.getRefreshToken();
  }

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
          next: tokens => this.#authRepository.storeTokens(tokens),
          error: error => console.error('Failed to register user', error),
        })
      );
  }

  me(): Observable<AuthUserModel> {
    return this.#authRepository.me();
  }

  logout(): void {
    this.#authRepository.clearTokens();
    void this.#appRouter.redirectToLogin();
  }
}
