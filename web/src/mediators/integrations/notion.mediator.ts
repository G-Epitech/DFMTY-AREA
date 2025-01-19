import { inject, Injectable } from '@angular/core';
import {
  NotionDatabaseDTO,
  NotionPageDTO,
  NotionRepository,
} from '@repositories/integrations';
import { map, Observable } from 'rxjs';
import { NotionDatabaseModel, NotionPageModel } from '@models/integration';

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
    return new NotionDatabaseModel(
      dto.id,
      dto.title,
      dto.description,
      dto.icon,
      dto.uri
    );
  }

  getPages(integrationId: string): Observable<NotionPageModel[]> {
    return this.#notionRepository
      .getPages(integrationId)
      .pipe(map(pages => pages.map(page => this._mapPageModel(page))));
  }

  _mapPageModel(page: NotionPageDTO): NotionPageModel {
    return new NotionPageModel(page.id, page.title, page.icon, page.uri);
  }
}
