import { inject, Injectable } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from './store';
import { AppRouter } from './app.router';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  readonly #authMediator = inject(AuthMediator);
  readonly #authStore = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);

  async appInit(): Promise<void> {
    const tokens = this.#authMediator.getTokens();
    if (!tokens.accessToken || !tokens.isAccessTokenValid) {
      void this.#appRouter.redirectToLogin();
      return;
    }
    this.#authStore.me();
    if (this.#authStore.isAuthenticated()) {
      void this.#appRouter.redirectToHome();
    } else {
      void this.#appRouter.redirectToLogin();
    }
  }
}
