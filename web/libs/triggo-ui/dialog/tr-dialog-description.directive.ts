import { Directive, computed, input } from '@angular/core';
import { hlm } from '@spartan-ng/ui-core';
import { BrnDialogDescriptionDirective } from '@spartan-ng/ui-dialog-brain';
import type { ClassValue } from 'clsx';

@Directive({
  selector: '[trDialogDescription]',
  standalone: true,
  host: {
    '[class]': '_computedClass()',
  },
  hostDirectives: [BrnDialogDescriptionDirective],
})
export class TrDialogDescriptionDirective {
  public readonly userClass = input<ClassValue>('', { alias: 'class' });
  protected _computedClass = computed(() =>
    hlm('text-sm text-muted-foreground', this.userClass())
  );
}
