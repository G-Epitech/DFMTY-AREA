import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import {
  IntegrationModel,
  IntegrationTypeEnum,
  IntegrationDiscordProps,
} from '@models/integration';
import { Observable, of } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { IntegrationLinkedCardComponent } from '@features/integrations/components/integration-linked/integration-linked-card.component';
import { IntegrationAddDialogComponent } from '@features/integrations/components/integration-add-dialog/integration-add-dialog.component';
import { PaginationComponent } from '@app/components';
import { TrInputDirective } from '@triggo-ui/input';
import { PageModel, PageOptions } from '@models/page';
import { UsersMediator } from '@mediators/users.mediator';
import { AuthStore } from '@app/store';

@Component({
  selector: 'tr-integrations',
  imports: [
    AsyncPipe,
    IntegrationLinkedCardComponent,
    IntegrationAddDialogComponent,
    PaginationComponent,
    TrInputDirective,
  ],
  templateUrl: './integrations.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationsPageComponent {
  readonly #usersMediator = inject(UsersMediator);
  readonly #store = inject(AuthStore);

  pageOptions = signal<PageOptions>({
    page: 1,
    size: 5,
  });

  readonly integrations: Observable<PageModel<IntegrationModel>> | null =
    this.#store.user()
      ? this.#usersMediator.getIntegrations(
          this.#store.user()!.id,
          this.pageOptions()
        )
      : null;
}
