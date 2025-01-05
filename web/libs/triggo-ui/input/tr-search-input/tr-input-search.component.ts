import {
  ChangeDetectionStrategy,
  Component,
  computed,
  input,
} from '@angular/core';
import { TrInputDirective } from '@triggo-ui/input';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { heroMagnifyingGlass } from '@ng-icons/heroicons/outline';
import type { ClassValue } from 'clsx';
import { hlm } from '@spartan-ng/ui-core';

@Component({
  selector: 'tr-input-search',
  imports: [TrInputDirective, NgIcon],
  templateUrl: './tr-input-search.component.html',
  styles: [],
  providers: [provideIcons({ heroMagnifyingGlass })],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  host: {
    '[class]': '_computedClass()',
  },
})
export class TrInputSearchComponent {
  placeholder = input<string>('');
  public readonly userClass = input<ClassValue>('', { alias: 'class' });

  protected _computedClass = computed(() => hlm('relative', this.userClass()));
}
