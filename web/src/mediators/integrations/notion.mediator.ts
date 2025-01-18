import { inject, Injectable } from '@angular/core';
import {
  NotionDatabaseDTO,
  NotionRepository,
} from '@repositories/integrations';
import { map, Observable } from 'rxjs';
import { NotionDatabaseModel } from '@models/integration';

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

  getDatabases(integationId: string): Observable<NotionDatabaseModel[]> {
    return this.#notionRepository
      .getDatabases(integationId)
      .pipe(map(databases => databases.map(db => this._mapDatabaseModel(db))));
  }

  _mapDatabaseModel(dto: NotionDatabaseDTO): NotionDatabaseModel {
    return {
      ...dto,
    } as NotionDatabaseModel;
  }
}
