import { inject, Injectable } from '@angular/core';
import { AuthStore } from './store';
import { TokenMediator } from '@mediators/token.mediator';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  readonly #authStore = inject(AuthStore);
  readonly #tokenMediator = inject(TokenMediator);

  async appInit(): Promise<void> {
    if (this.#tokenMediator.accessTokenIsValid()) {
      this.#authStore.me();
    } else {
      this.#authStore.cancel();
    }
  }
}
