import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';
import { PageModel, PageOptions } from '@models/page';
import { map, Observable } from 'rxjs';
import { IntegrationModel } from '@models/integration';
import { AutomationModel } from '@models/automation';

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
    pageOptions: PageOptions
  ): Observable<PageModel<IntegrationModel>> {
    return this.#usersRepository.getIntegrations(pageOptions).pipe(
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
      })
    );
  }

  getAutomations(
    pageOptions: PageOptions
  ): Observable<PageModel<AutomationModel>> {
    return this.#usersRepository.getAutomations(pageOptions).pipe(
      map(res => {
        return {
          ...res,
          data: res.data.map(automation => {
            return new AutomationModel(
              automation.id,
              automation.ownerId,
              automation.label,
              automation.description,
              automation.enabled,
              automation.updatedAt,
              '#EE883A',
              'bolt',
              automation.trigger,
              automation.actions
            );
          }),
        };
      })
    );
  }
}
