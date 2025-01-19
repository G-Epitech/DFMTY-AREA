import { CdkObserveContent } from '@angular/cdk/observers';
import {
  Component,
  ContentChildren,
  type ElementRef,
  type QueryList,
  ViewChild,
  computed,
  input,
  ChangeDetectionStrategy,
} from '@angular/core';
import { heroChevronLeft, heroChevronRight } from '@ng-icons/heroicons/outline';
import { buttonVariants } from '@triggo-ui/button';
import { hlm } from '@spartan-ng/ui-core';
import { TrIconComponent, provideIcons } from '@triggo-ui/icon';
import {
  BrnTabsPaginatedListDirective,
  BrnTabsTriggerDirective,
} from '@spartan-ng/ui-tabs-brain';
import type { ClassValue } from 'clsx';
import { listVariants } from './tr-tabs-list.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'tr-paginated-tabs-list',
  standalone: true,
  imports: [CdkObserveContent, TrIconComponent],
  providers: [provideIcons({ heroChevronRight, heroChevronLeft })],
  template: `
    <button
      #previousPaginator
      data-pagination="previous"
      type="button"
      aria-hidden="true"
      tabindex="-1"
      [class.flex]="_showPaginationControls()"
      [class.hidden]="!_showPaginationControls()"
      [class]="_paginationButtonClass()"
      [disabled]="_disableScrollBefore || null"
      (click)="_handlePaginatorClick('before')"
      (mousedown)="_handlePaginatorPress('before', $event)"
      (touchend)="_stopInterval()">
      <tr-icon size="base" name="heroChevronLeft" />
    </button>

    <div
      #tabListContainer
      class="z-[1] flex grow overflow-hidden"
      (keydown)="_handleKeydown($event)">
      <div
        class="relative grow transition-transform"
        #tabList
        role="tablist"
        (cdkObserveContent)="_onContentChanges()">
        <div #tabListInner [class]="_tabListClass()">
          <ng-content></ng-content>
        </div>
      </div>
    </div>

    <button
      #nextPaginator
      data-pagination="next"
      type="button"
      aria-hidden="true"
      tabindex="-1"
      [class.flex]="_showPaginationControls()"
      [class.hidden]="!_showPaginationControls()"
      [class]="_paginationButtonClass()"
      [disabled]="_disableScrollAfter || null"
      (click)="_handlePaginatorClick('after')"
      (mousedown)="_handlePaginatorPress('after', $event)"
      (touchend)="_stopInterval()">
      <tr-icon size="base" name="heroChevronRight" />
    </button>
  `,
  host: {
    '[class]': '_computedClass()',
  },
})
export class TrTabsPaginatedListComponent extends BrnTabsPaginatedListDirective {
  @ContentChildren(BrnTabsTriggerDirective, { descendants: false })
  _items!: QueryList<BrnTabsTriggerDirective>;
  @ViewChild('tabListContainer', { static: true })
  _tabListContainer!: ElementRef;
  @ViewChild('tabList', { static: true }) _tabList!: ElementRef;
  @ViewChild('tabListInner', { static: true }) _tabListInner!: ElementRef;
  @ViewChild('nextPaginator') _nextPaginator!: ElementRef<HTMLElement>;
  @ViewChild('previousPaginator') _previousPaginator!: ElementRef<HTMLElement>;

  public readonly userClass = input<ClassValue>('', { alias: 'class' });
  protected _computedClass = computed(() =>
    hlm('flex overflow-hidden relative flex-shrink-0', this.userClass())
  );

  public readonly tabLisClass = input<ClassValue>('', { alias: 'class' });
  protected _tabListClass = computed(() =>
    hlm(listVariants(), this.tabLisClass())
  );

  public readonly paginationButtonClass = input<ClassValue>('', {
    alias: 'class',
  });
  protected _paginationButtonClass = computed(() =>
    hlm(
      'relative z-[2] select-none data-[pagination=previous]:pr-1 data-[pagination=next]:pl-1 disabled:cursor-default',
      buttonVariants({ variant: 'ghost', size: 'icon' }),
      this.paginationButtonClass()
    )
  );

  protected _itemSelected(event: KeyboardEvent) {
    event.preventDefault();
  }
}
