import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  signal,
} from '@angular/core';
import { IntegrationModel } from '@models/integration';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { IntegrationLinkedCardComponent } from '@features/integrations/components/integration-linked/integration-linked-card.component';
import { IntegrationAddDialogComponent } from '@features/integrations/components/integration-add-dialog/integration-add-dialog.component';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { PageModel, PageOptions } from '@models/page';
import { UsersMediator } from '@mediators/users.mediator';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';

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

  pageOptions = signal<PageOptions>({
    page: 0,
    size: 5,
  });
  totalPages = signal<number>(3);
  loading = signal<boolean>(true);

  #pageOptionsSubject = new BehaviorSubject<PageOptions>(this.pageOptions());

  readonly integrations: Observable<PageModel<IntegrationModel>> =
    this.#usersMediator.getIntegrations(this.pageOptions()).pipe(
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
