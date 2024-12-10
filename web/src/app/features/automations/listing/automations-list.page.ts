import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { Observable, of } from 'rxjs';
import { PageModel } from '@models/page';
import { AutomationModel } from '@models/automation.model';
import { AsyncPipe } from '@angular/common';
import { AutomationCardComponent } from '@features/automations/listing/components/automation-card/automation-card.component';

@Component({
  selector: 'tr-automations-list',
  imports: [
    TrButtonDirective,
    PaginationComponent,
    TrInputSearchComponent,
    AsyncPipe,
    AutomationCardComponent,
  ],
  templateUrl: './automations-list.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsListPageComponent {
  totalPages = signal<number>(3);

  automations: Observable<PageModel<AutomationModel>>;

  constructor() {
    this.automations = of({
      pageNumber: 1,
      pageSize: 10,
      totalPages: 3,
      totalRecords: 21,
      data: [
        new AutomationModel(
          '1',
          'Reply “Feur” to “Quoi”',
          'Description 1',
          true,
          new Date(),
          '#EE883A',
          'chat-bubble-bottom-center-text'
        ),
        new AutomationModel(
          '1',
          'Play spotify at home',
          'Description 1',
          false,
          new Date(),
          '#EE3A3D',
          'sparkles'
        ),
      ],
    });
  }
}
