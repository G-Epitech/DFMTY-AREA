import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';

@Injectable({
  providedIn: 'root',
})
export class UsersMediator {
  readonly #usersRepository = inject(UsersRepository);

  getById(id: string) {
    return this.#usersRepository.getById(id);
  }

  me() {
    return this.#usersRepository.getUser();
  }
}
