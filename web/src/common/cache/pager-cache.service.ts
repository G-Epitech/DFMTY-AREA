import { computed, Injectable, Signal, signal } from '@angular/core';
import { PageModel, PageOptions } from '@models/page';

@Injectable()
export class PagerCacheService<T> {
  private pageCache = signal<Record<string, PageModel<T> | null>>({});

  clear(): void {
    this.pageCache.set({});
  }

  clearLastPage(): void {
    this.pageCache.update(cache => {
      const keys = Object.keys(cache);
      const lastKey = keys[keys.length - 1];
      delete cache[lastKey];
      return cache;
    });
  }

  getPage(pageOptions: PageOptions): Signal<PageModel<T> | null> {
    return computed(() => {
      const cacheKey = JSON.stringify(pageOptions);
      return this.pageCache()[cacheKey] as PageModel<T> | null;
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
