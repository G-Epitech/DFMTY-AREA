import {
  ChangeDetectionStrategy,
  Component,
  ViewEncapsulation,
  forwardRef,
} from '@angular/core';
import {
  BrnDialogComponent,
  BrnDialogOverlayComponent,
} from '@spartan-ng/ui-dialog-brain';
import { TrDialogOverlayDirective } from './tr-dialog-overlay.directive';

@Component({
  selector: 'tr-dialog',
  standalone: true,
  imports: [
    BrnDialogOverlayComponent,
    TrDialogOverlayDirective,
  ],
  providers: [
    {
      provide: BrnDialogComponent,
      useExisting: forwardRef(() => TrDialogComponent),
    },
  ],
  template: `
    <brn-dialog-overlay hlm />
    <ng-content />
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  exportAs: 'hlmDialog',
})
export class TrDialogComponent extends BrnDialogComponent {
  constructor() {
    super();
    this.closeDelay = 100;
  }
}
