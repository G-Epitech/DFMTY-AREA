import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

@Component({
  standalone: true,
  selector: 'tr-pagination',
  imports: [TrButtonDirective, NgIcon],
  templateUrl: './pagination.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginationComponent {
  totalPages = input.required<number>();

  get pages(): number[] {
    return Array.from({ length: this.totalPages() }, (_, i) => i + 1);
  }
}
