import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';
import { TokenMediator } from '@mediators/token.mediator';

@Injectable({
  providedIn: 'root',
})
export class UsersMediator {
  readonly #usersRepository = inject(UsersRepository);
  readonly #tokenMediator = inject(TokenMediator);

  getById(id: string) {
    return this.#usersRepository.getById(id);
  }

  me() {
    const tokens = this.#tokenMediator.getTokens();
    const userId = tokens.userId;
    if (!userId) {
      throw new Error('No user ID found in tokens');
    }
    return this.getById(userId);
  }
}
