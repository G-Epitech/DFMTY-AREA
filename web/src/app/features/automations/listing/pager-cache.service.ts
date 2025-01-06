import { computed, Injectable, signal } from '@angular/core';
import { PageModel, PageOptions } from '@models/page';

@Injectable({ providedIn: 'root' })
export class PagerCacheService<T> {
  private pageCache = signal<Record<string, PageModel<T> | null>>({});

  getPage(pageOptions: PageOptions) {
    return computed(() => {
      const cacheKey = JSON.stringify(pageOptions);
      return this.pageCache()[cacheKey];
    });
  }

  setPage(pageOptions: PageOptions, data: PageModel<T>): void {
    const cacheKey = JSON.stringify(pageOptions);
    this.pageCache.update(cache => ({
      ...cache,
      [cacheKey]: data,
    }));
  }
}
