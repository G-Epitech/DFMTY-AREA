import { inject, Injectable } from '@angular/core';
import { LeagueOfLegendsRepository } from '@repositories/integrations';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LeagueOfLegendsMediator {
  readonly #repository = inject(LeagueOfLegendsRepository);

  link(gameName: string, tagLine: string): Observable<void> {
    return this.#repository.link({
      gameName: gameName,
      tagLine: tagLine,
    });
  }
}
