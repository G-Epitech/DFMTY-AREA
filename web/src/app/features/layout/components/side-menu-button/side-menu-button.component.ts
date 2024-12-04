import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-side-menu-button',
  imports: [NgIcon, TrButtonDirective],
  templateUrl: './side-menu-button.component.html',
  styleUrl: './side-menu-button.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SideMenuButtonComponent {
  label = input.required<string>();
  icon = input.required<string>();
}
