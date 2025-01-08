import { inject, Injectable } from '@angular/core';
import { AuthStore } from './store';
import { TokenMediator } from '@mediators/token.mediator';
import { SchemaStore } from '@app/store/schema-store';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  readonly #authStore = inject(AuthStore);
  readonly #schemaStore = inject(SchemaStore);
  readonly #tokenMediator = inject(TokenMediator);

  async appInit(): Promise<void> {
    this.#schemaStore.initialize();
    if (this.#tokenMediator.accessTokenIsValid()) {
      this.#authStore.me();
    } else {
      this.#authStore.cancel();
    }
  }
}
