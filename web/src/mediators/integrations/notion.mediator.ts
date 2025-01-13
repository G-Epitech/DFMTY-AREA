import { inject, Injectable } from '@angular/core';
import { NotionRepository } from '@repositories/integrations';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NotionMediator {
  readonly #notionRepository = inject(NotionRepository);

  getUri(): Observable<string> {
    return this.#notionRepository.getUri().pipe(map(res => res.uri));
  }

  link(state: string, code: string): Observable<void> {
    return this.#notionRepository.link({
      state: state,
      code: code,
    });
  }
}
