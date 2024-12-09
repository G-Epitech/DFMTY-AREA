import {
  ChangeDetectionStrategy,
  Component,
  input,
  signal,
} from '@angular/core';
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
  selectedPage = signal<number>(1);

  get pages(): number[] {
    return Array.from({ length: this.totalPages() }, (_, i) => i + 1);
  }

  selectPage(page: number): void {
    if (page >= 1 && page <= this.totalPages()) {
      this.selectedPage.set(page);
    }
  }

  previousPage(): void {
    if (this.selectedPage() > 1) {
      this.selectedPage.update(current => current - 1);
    }
  }

  nextPage(): void {
    if (this.selectedPage() < this.totalPages()) {
      this.selectedPage.update(current => current + 1);
    }
  }
}
