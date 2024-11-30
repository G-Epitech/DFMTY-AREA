import { inject, Injectable } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from './store';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  readonly #authMediator = inject(AuthMediator);
  readonly #authStore = inject(AuthStore);
  readonly #router = inject(Router);

  async appInit(): Promise<void> {
    const tokens = this.#authMediator.getTokens();
    if (!tokens.accessToken || !tokens.isAccessTokenValid) {
      void this.#router.navigate(['/register']);
      return;
    }
    this.#authStore.me();
    if (this.#authStore.isAuthenticated()) {
      void this.#router.navigate(['/home']);
    } else {
      void this.#router.navigate(['/register']);
    }
  }
}
