import { inject, Injectable } from '@angular/core';
import { GithubRepository } from '@repositories/integrations';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GithubMediator {
  readonly #repository = inject(GithubRepository);

  getUri(): Observable<string> {
    return this.#repository.getUri().pipe(map(res => res.uri));
  }

  link(state: string, code: string): Observable<void> {
    return this.#repository.link({
      state: state,
      code: code,
    });
  }
}
