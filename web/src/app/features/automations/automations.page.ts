import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { PaginationComponent } from '@app/components';
import { TrInputSearchComponent } from '@triggo-ui/input';

@Component({
  selector: 'tr-automations',
  imports: [TrButtonDirective, PaginationComponent, TrInputSearchComponent],
  templateUrl: './automations.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsPageComponent {
  totalPages = signal<number>(21);
}
