import { inject, Injectable } from '@angular/core';
import { OpenaiRepository } from '@repositories/integrations/openai.repository';
import { map, Observable } from 'rxjs';
import { OpenaiModel } from '@models/integration';

@Injectable({
  providedIn: 'root',
})
export class OpenaiMediator {
  readonly #openaiRepository = inject(OpenaiRepository);

  link(apiToken: string, adminApiToken: string): Observable<void> {
    return this.#openaiRepository.link({
      apiToken: apiToken,
      adminApiToken: adminApiToken,
    });
  }

  getModels(integrationId: string): Observable<OpenaiModel[]> {
    return this.#openaiRepository.getModels(integrationId).pipe(
      map(models =>
        models.map(model => {
          return {
            id: model.id,
          } as OpenaiModel;
        })
      )
    );
  }
}
