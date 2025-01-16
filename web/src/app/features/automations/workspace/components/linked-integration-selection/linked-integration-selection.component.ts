import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  input, output,
  signal,
} from '@angular/core';
import { BehaviorSubject, map, Observable, startWith, tap } from 'rxjs';
import { PageModel, PageOptions } from '@models/page';
import { UsersMediator } from '@mediators/users.mediator';
import { IntegrationModel } from '@models/integration';
import { AsyncPipe } from '@angular/common';
import { LinkedIntegrationButtonComponent } from '@components/linked-integration-button/linked-integration-button.component';
import { finalize, switchMap } from 'rxjs/operators';
import { integrationTypeFromIdentifier } from '@utils/integration';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';

@Component({
  selector: 'tr-linked-integration-selection',
  imports: [AsyncPipe, LinkedIntegrationButtonComponent, TrSkeletonComponent],
  templateUrl: './linked-integration-selection.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LinkedIntegrationSelectionComponent {
  readonly #usersMediator = inject(UsersMediator);

  loading = signal<boolean>(true);
  selectedIntegrationIdentifier = input.required<string>();
  selectedIntegrationType = computed(() => {
    if (this.selectedIntegrationIdentifier()) {
      return integrationTypeFromIdentifier(
        this.selectedIntegrationIdentifier()
      );
    }
    return null;
  });
  selectedLinkedIntegration = output<IntegrationModel>();

  pageOptions$: BehaviorSubject<PageOptions> = new BehaviorSubject<PageOptions>(
    {
      page: 0,
      size: 5,
    }
  );

  readonly integrations$: Observable<PageModel<IntegrationModel>> =
    this.pageOptions$.pipe(
      tap(() => this.loading.set(true)),
      switchMap(pageOptions =>
        this.#usersMediator.getIntegrations(pageOptions).pipe(
          map(page => ({
            ...page,
            data: page.data.filter(
              integration => integration.type === this.selectedIntegrationType()
            ),
          })),
          tap(() => this.loading.set(false)),
          finalize(() => this.loading.set(false))
        )
      ),
    );

  selectLinkedIntegration(integration: IntegrationModel) {
    this.selectedLinkedIntegration.emit(integration);
  }
}
