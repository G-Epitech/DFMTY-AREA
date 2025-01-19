import {
  Component,
  computed,
  input,
  ChangeDetectionStrategy,
} from '@angular/core';
import { hlm } from '@spartan-ng/ui-core';
import { BrnTabsListDirective } from '@spartan-ng/ui-tabs-brain';
import { type VariantProps, cva } from 'class-variance-authority';
import type { ClassValue } from 'clsx';

export const listVariants = cva(
  'inline-flex items-center justify-center rounded-md bg-secondary p-1 text-muted-black',
  {
    variants: {
      orientation: {
        horizontal: 'h-10 space-x-1',
        vertical: 'mt-2 flex-col h-fit space-y-1',
      },
    },
    defaultVariants: {
      orientation: 'horizontal',
    },
  }
);
type ListVariants = VariantProps<typeof listVariants>;

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'tr-tabs-list',
  standalone: true,
  hostDirectives: [BrnTabsListDirective],
  template: '<ng-content/>',
  host: {
    '[class]': '_computedClass()',
  },
})
export class TrTabsListComponent {
  public readonly orientation =
    input<ListVariants['orientation']>('horizontal');

  public readonly userClass = input<ClassValue>('', { alias: 'class' });
  protected _computedClass = computed(() =>
    hlm(listVariants({ orientation: this.orientation() }), this.userClass())
  );
}
