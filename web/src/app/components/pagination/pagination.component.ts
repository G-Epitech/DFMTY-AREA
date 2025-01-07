import {
  ChangeDetectionStrategy,
  Component,
  effect,
  input,
  output,
  signal,
} from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

@Component({
  standalone: true,
  selector: 'tr-pagination',
  imports: [TrButtonDirective, NgIcon],
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginationComponent {
  totalPages = input.required<number>();
  selectedPage = signal<number>(1);

  pageChange = output<number>();

  get pages(): number[] {
    return Array.from({ length: this.totalPages() }, (_, i) => i + 1);
  }

  constructor() {
    effect(() => {
      this.pageChange.emit(this.selectedPage() - 1);
    });
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
