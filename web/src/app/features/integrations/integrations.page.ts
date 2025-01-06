import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  signal,
} from '@angular/core';
import { IntegrationModel } from '@models/integration';
import { BehaviorSubject, Observable, of, switchMap, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { IntegrationLinkedCardComponent } from '@features/integrations/components/integration-linked/integration-linked-card.component';
import { IntegrationAddDialogComponent } from '@features/integrations/components/integration-add-dialog/integration-add-dialog.component';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { PageModel, PageOptions } from '@models/page';
import { UsersMediator } from '@mediators/users.mediator';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';
import { INTEGRATION_CACHE_SERVICE } from '@common/cache/injection-tokens';
import { PagerCacheService } from '@common/cache/pager-cache.service';

@Component({
  selector: 'tr-integrations',
  imports: [
    AsyncPipe,
    IntegrationLinkedCardComponent,
    IntegrationAddDialogComponent,
    PaginationComponent,
    TrSkeletonComponent,
    TrInputSearchComponent,
  ],
  templateUrl: './integrations.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationsPageComponent {
  readonly #usersMediator = inject(UsersMediator);
  readonly #cacheService: PagerCacheService<IntegrationModel> = inject(
    INTEGRATION_CACHE_SERVICE
  );

  pageOptions$: BehaviorSubject<PageOptions> = new BehaviorSubject<PageOptions>(
    {
      page: 0,
      size: 5,
    }
  );

  totalPages = signal<number>(3);
  loading = signal<boolean>(true);

  readonly integrations: Observable<PageModel<IntegrationModel>> =
    this.pageOptions$.pipe(
      switchMap(pageOptions => {
        const cachedPage = this.#cacheService.getPage(pageOptions)();
        if (cachedPage) {
          this.loading.set(false);
          return of(cachedPage);
        }
        this.loading.set(true);
        return this.#usersMediator.getIntegrations(pageOptions).pipe(
          tap(page => {
            this.totalPages.set(page.totalPages);
            this.loading.set(false);
            this.#cacheService.setPage(pageOptions, page);
          })
        );
      })
    );

  pageChanged(page: number): void {
    this.loading.set(true);
    this.pageOptions$.next({ ...this.pageOptions$.value, page });
  }
}
