import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';
import { PageModel, PageOptions } from '@models/page';
import { map, Observable } from 'rxjs';
import { IntegrationModel } from '@models/integration';
import { AutomationModel } from '@models/automation';
import { UserModel } from '@models/user.model';
import { AutomationMapperService } from '@mediators/mappers';

@Injectable({
  providedIn: 'root',
})
export class UsersMediator {
  readonly #usersRepository = inject(UsersRepository);
  readonly #automationMapper = inject(AutomationMapperService);

  getById(id: string): Observable<UserModel> {
    return this.#usersRepository
      .getById(id)
      .pipe(
        map(
          res =>
            new UserModel(
              res.id,
              res.email,
              res.firstName,
              res.lastName,
              res.picture
            )
        )
      );
  }

  me(): Observable<UserModel> {
    return this.#usersRepository
      .getUser()
      .pipe(
        map(
          res =>
            new UserModel(
              res.id,
              res.email,
              res.firstName,
              res.lastName,
              res.picture
            )
        )
      );
  }

  getIntegrations(
    pageOptions: PageOptions
  ): Observable<PageModel<IntegrationModel>> {
    return this.#usersRepository.getIntegrations(pageOptions).pipe(
      map(res => ({
        ...res,
        data: res.data.map(
          integration =>
            new IntegrationModel(
              integration.id,
              integration.ownerId,
              integration.isValid,
              integration.type,
              integration.properties
            )
        ),
      }))
    );
  }

  getAutomations(
    pageOptions: PageOptions
  ): Observable<PageModel<AutomationModel>> {
    return this.#usersRepository.getAutomations(pageOptions).pipe(
      map(res => ({
        ...res,
        data: res.data.map(automation =>
          this.#automationMapper.mapAutomationModel(automation)
        ),
      }))
    );
  }
}
