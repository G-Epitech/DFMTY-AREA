import { inject, Injectable } from '@angular/core';
import { UsersRepository } from '@repositories/users';
import { PageModel, PageOptions } from '@models/page';
import { map, Observable } from 'rxjs';
import { IntegrationModel } from '@models/integration';
import {
  ActionShortModel,
  AutomationModel,
  TriggerShortModel,
} from '@models/automation';
import { UserModel } from '@models/user.model';
import { ActionShortDTO } from '@repositories/automations/dto';

@Injectable({
  providedIn: 'root',
})
export class UsersMediator {
  readonly #usersRepository = inject(UsersRepository);

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
        data: res.data.map(
          automation =>
            new AutomationModel(
              automation.id,
              automation.ownerId,
              automation.label,
              automation.description,
              automation.enabled,
              automation.updatedAt,
              '#EE883A',
              'chat-bubble-bottom-center-text',
              new TriggerShortModel(
                automation.trigger.identifier,
                automation.trigger.parameters,
                automation.trigger.providers
              ),
              this._mapActions(automation.actions)
            )
        ),
      }))
    );
  }

  _mapActions(actions: ActionShortDTO[]): ActionShortModel[] {
    return actions.map(
      action =>
        new ActionShortModel(
          action.identifier,
          action.parameters,
          action.providers
        )
    );
  }
}
