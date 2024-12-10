import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  UserAutomationsGetResponseDTO,
  UsersGetResponseDTO,
} from '@repositories/users/dto';
import { map, Observable, of } from 'rxjs';
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
    userId: string,
    pageOptions: PageOptions
  ): Observable<UserIntegrationsGetResponseDTO> {
    const searchParams = pageOptionsToParams(pageOptions);
    const url = `${this.baseUrl}/users/${userId}/integrations?${searchParams}`;
    return this.#httpClient.get<UserIntegrationsGetResponseDTO>(url);
  }

  getAutomations(
    userId: string,
    pageOptions: PageOptions
  ): Observable<UserAutomationsGetResponseDTO> {
    const data: UserAutomationsGetResponseDTO = {
      pageNumber: 1,
      pageSize: 5,
      totalRecords: 20,
      totalPages: 4,
      data: [
        {
          id: '1',
          label: 'DigitalOcean Droplet',
          description: 'DigitalOcean Droplet Automation',
          ownerId: 'userId',
          enabled: true,
          updatedAt: new Date(),
          trigger: {
            id: '1',
            identifier: 'DigitalOcean',
            providers: ['droplet'],
            parameters: [],
          },
          actions: [
            {
              id: '1',
              identifier: 'Slack',
              providers: ['send-message'],
              parameters: [],
            },
          ],
        },
      ],
    };
    return of(data);
  }
}
