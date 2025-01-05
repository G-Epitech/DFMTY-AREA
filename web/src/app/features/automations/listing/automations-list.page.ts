import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  OnDestroy,
  signal,
} from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import {
  BehaviorSubject,
  delay,
  Observable,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';
import { PageModel, PageOptions } from '@models/page';
import { AutomationModel } from '@models/automation/automation.model';
import { AsyncPipe } from '@angular/common';
import { AutomationCardComponent } from '@features/automations/listing/components/automation-card/automation-card.component';
import { UsersMediator } from '@mediators/users.mediator';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';
import { AutomationsMediator } from '@mediators/automations.mediator';
import { ToastrService } from 'ngx-toastr';

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
export class AutomationsListPageComponent implements OnDestroy {
  readonly #usersMediator = inject(UsersMediator);
  readonly #automationsMediator = inject(AutomationsMediator);
  readonly #toastr = inject(ToastrService);

  private destroy$ = new Subject<void>();

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

  createAutomation(): void {
    this.#automationsMediator
      .create()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        error: () => {
          this.#toastr.error('Error creating automation');
        },
        next: () => {
          this.#toastr.success('Automation created');
          window.location.reload();
        },
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
