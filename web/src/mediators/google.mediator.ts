import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { GoogleAuthConfigurationModel } from '@models/google-auth-configuration.model';
import { GoogleRepository } from '@repositories/integrations/google.repository';

@Injectable({
  providedIn: 'root',
})
export class GoogleMediator {
  readonly #googleRepository = inject(GoogleRepository);

  getGoogleConfiguration(): Observable<GoogleAuthConfigurationModel> {
    return this.#googleRepository
      .getGoogleConfiguration()
      .pipe(
        map(
          res =>
            new GoogleAuthConfigurationModel(
              res.scopes,
              res.clientId,
              res.endpoint
            )
        )
      );
  }
}
