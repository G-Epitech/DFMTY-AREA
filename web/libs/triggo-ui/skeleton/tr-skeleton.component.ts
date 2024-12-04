import {
  Component,
  computed,
  input,
  ChangeDetectionStrategy,
} from '@angular/core';
import { hlm } from '@spartan-ng/ui-core';
import type { ClassValue } from 'clsx';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'tr-skeleton',
  standalone: true,
  template: '',
  host: {
    '[class]': '_computedClass()',
  },
})
export class TrSkeletonComponent {
  public readonly userClass = input<ClassValue>('', { alias: 'class' });
  protected _computedClass = computed(() =>
    hlm('block animate-pulse rounded-md bg-muted', this.userClass())
  );
}
