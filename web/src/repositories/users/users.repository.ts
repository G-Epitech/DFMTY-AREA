import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  UserAutomationsGetResponseDTO,
  UsersGetResponseDTO,
} from '@repositories/users/dto';
import { map, Observable } from 'rxjs';
import { UserModel } from '@models/user.model';
import { UserIntegrationsGetResponseDTO } from '@repositories/users/dto/user-integrations-get.dto';
import { PageOptions } from '@models/page';
import { pageOptionsToParams } from '@utils/params';

@Injectable({
  providedIn: 'root',
})
export class UsersRepository {
  readonly #httpClient = inject(HttpClient);

  constructor(@Inject('BASE_URL') private baseUrl: string) {}

  getById(id: string) {
    const url = `${this.baseUrl}/users/${id}`;
    const response = this.#httpClient.get<UsersGetResponseDTO>(url);
    return response.pipe(
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

  getUser() {
    const url = `${this.baseUrl}/user`;
    const response = this.#httpClient.get<UsersGetResponseDTO>(url);
    return response.pipe(
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
  ): Observable<UserIntegrationsGetResponseDTO> {
    const searchParams = pageOptionsToParams(pageOptions);
    const url = `${this.baseUrl}/user/integrations?${searchParams}`;
    return this.#httpClient.get<UserIntegrationsGetResponseDTO>(url);
  }

  getAutomations(
    pageOptions: PageOptions
  ): Observable<UserAutomationsGetResponseDTO> {
    const searchParams = pageOptionsToParams(pageOptions);
    const url = `${this.baseUrl}/user/automations?${searchParams}`;
    return this.#httpClient.get<UserAutomationsGetResponseDTO>(url);
  }
}
