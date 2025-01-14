import { inject, Injectable } from '@angular/core';
import { OpenaiRepository } from '@repositories/integrations/openai.repository';
import { Observable } from 'rxjs';

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
}
