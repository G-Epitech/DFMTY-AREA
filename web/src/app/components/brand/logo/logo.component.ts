import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  selector: 'tr-logo',
  imports: [],
  templateUrl: './logo.component.html',
  styles: [],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LogoComponent {
  width = input(215);
  height = input(72);
}
