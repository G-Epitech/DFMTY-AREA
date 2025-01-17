import {
  ChangeDetectionStrategy,
  Component,
  inject,
  signal,
} from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { Observable, of, switchMap, tap, BehaviorSubject } from 'rxjs';
import { PageModel, PageOptions } from '@models/page';
import { AutomationModel } from '@models/automation/automation.model';
import { AsyncPipe } from '@angular/common';
import { AutomationCardComponent } from '@features/automations/listing/components/automation-card/automation-card.component';
import { UsersMediator } from '@mediators/users.mediator';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';
import { NgIcon } from '@ng-icons/core';
import { PagerCacheService } from '@common/cache/pager-cache.service';
import { AUTOMATION_CACHE_SERVICE } from '@common/cache/injection-tokens';
import { AppRouter } from '@app/app.router';

@Component({
  selector: 'tr-automations-list',
  imports: [
    TrButtonDirective,
    PaginationComponent,
    TrInputSearchComponent,
    AsyncPipe,
    AutomationCardComponent,
    TrSkeletonComponent,
    NgIcon,
  ],
  templateUrl: './automations-list.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsListPageComponent {
  readonly #usersMediator = inject(UsersMediator);
  readonly #cacheService: PagerCacheService<AutomationModel> = inject(
    AUTOMATION_CACHE_SERVICE
  );
  readonly #appRouter = inject(AppRouter);

  pageOptions$: BehaviorSubject<PageOptions> = new BehaviorSubject<PageOptions>(
    {
      page: 0,
      size: 5,
    }
  );

  totalPages = signal(3);
  loading = signal(true);

  readonly automations: Observable<PageModel<AutomationModel>> =
    this.pageOptions$.pipe(
      switchMap(pageOptions => {
        const cachedPage = this.#cacheService.getPage(pageOptions)();
        if (cachedPage) {
          this.totalPages.set(cachedPage.totalPages);
          this.loading.set(false);
          return of(cachedPage);
        }
        this.loading.set(true);
        return this.#usersMediator.getAutomations(pageOptions).pipe(
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

  createAutomation(): void {
    this.#appRouter.redirectToAutomationWorkspace();
  }
}
