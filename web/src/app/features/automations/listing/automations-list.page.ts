import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  signal,
} from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import {
  BehaviorSubject,
  concat,
  delay,
  Observable,
  of,
  switchMap,
  tap,
} from 'rxjs';
import { PageModel, PageOptions } from '@models/page';
import { AutomationModel } from '@models/automation/automation.model';
import { AsyncPipe } from '@angular/common';
import { AutomationCardComponent } from '@features/automations/listing/components/automation-card/automation-card.component';
import { UsersMediator } from '@mediators/users.mediator';
import { IntegrationModel } from '@models/integration';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';

@Component({
  selector: 'tr-automations-list',
  imports: [
    TrButtonDirective,
    PaginationComponent,
    TrInputSearchComponent,
    AsyncPipe,
    AutomationCardComponent,
    TrSkeletonComponent,
  ],
  templateUrl: './automations-list.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsListPageComponent {
  readonly #usersMediator = inject(UsersMediator);

  pageOptions = signal<PageOptions>({
    page: 0,
    size: 5,
  });
  totalPages = signal<number>(3);
  loading = signal<boolean>(true);

  #pageOptionsSubject = new BehaviorSubject<PageOptions>(this.pageOptions());

  readonly automations: Observable<PageModel<AutomationModel>> =
    this.#usersMediator.getAutomations(this.pageOptions()).pipe(
      delay(1000),
      tap(page => {
        this.totalPages.set(page.totalPages);
        this.loading.set(false);
      })
    );

  constructor() {
    effect(() => {
      const currentPageOptions = this.pageOptions();
      this.#pageOptionsSubject.next(currentPageOptions);
    });
  }

  pageChanged(page: number): void {
    this.loading.set(true);
    this.pageOptions.update(options => ({
      size: options.size,
      page: page - 1,
    }));
  }
}
