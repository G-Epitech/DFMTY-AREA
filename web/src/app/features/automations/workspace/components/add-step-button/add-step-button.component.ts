import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-add-step-button',
  imports: [NgIcon, TrButtonDirective],
  templateUrl: './add-step-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AddStepButtonComponent {}
