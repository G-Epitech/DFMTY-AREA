import { inject, Injectable } from '@angular/core';
import { AuthStore } from './store';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  readonly #authStore = inject(AuthStore);

  async appInit(): Promise<void> {
    this.#authStore.me();
  }
}
