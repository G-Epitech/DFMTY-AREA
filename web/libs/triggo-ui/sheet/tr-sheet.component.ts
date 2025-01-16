import {
  ChangeDetectionStrategy,
  Component,
  ViewEncapsulation,
  forwardRef,
} from '@angular/core';
import { BrnDialogComponent } from '@spartan-ng/ui-dialog-brain';
import {
  BrnSheetComponent,
  BrnSheetOverlayComponent,
} from '@spartan-ng/ui-sheet-brain';
import { TrSheetOverlayDirective } from './tr-sheet-overlay.directive';

@Component({
  selector: 'tr-sheet',
  standalone: true,
  imports: [BrnSheetOverlayComponent, TrSheetOverlayDirective],
  providers: [
    {
      provide: BrnDialogComponent,
      useExisting: forwardRef(() => BrnSheetComponent),
    },
    {
      provide: BrnSheetComponent,
      useExisting: forwardRef(() => TrSheetComponent),
    },
  ],
  template: `
    <brn-sheet-overlay hlm />
    <ng-content />
  `,
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  exportAs: 'trSheet',
})
export class TrSheetComponent extends BrnSheetComponent {
  constructor() {
    super();
    this.closeDelay = 100;
  }
}
