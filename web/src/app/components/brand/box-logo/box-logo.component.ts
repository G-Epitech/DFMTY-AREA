import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  selector: 'tr-box-logo',
  imports: [],
  templateUrl: './box-logo.component.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BoxLogoComponent {
  width = input(300);
  height = input(300);
}
