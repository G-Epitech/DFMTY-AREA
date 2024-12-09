import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';
import { PageModel, PageOptions } from '@models/page';
import { map, Observable, tap } from 'rxjs';
import { IntegrationModel } from '@models/integration';

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

  getIntegrations(
    userId: string,
    pageOptions: PageOptions
  ): Observable<PageModel<IntegrationModel>> {
    return this.#usersRepository.getIntegrations(userId, pageOptions).pipe(
      tap(dto => console.log(dto)),
      map(res => {
        return {
          ...res,
          data: res.data.map(integration => {
            return new IntegrationModel(
              integration.id,
              integration.ownerId,
              integration.isValid,
              integration.type,
              integration.properties
            );
          }),
        };
      }),
      tap(res => console.log(res))
    );
  }
}
