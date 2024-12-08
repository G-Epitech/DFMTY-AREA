import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UsersGetResponseDTO } from '@repositories/users/dto';
import { map } from 'rxjs';
import { UserModel } from '@models/user.model';

@Injectable({
  providedIn: 'root',
})
export class IntegrationsRepository {
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
}
