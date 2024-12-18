import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'tr-side-menu-button',
  imports: [NgIcon, TrButtonDirective, RouterLink, RouterLinkActive],
  templateUrl: './side-menu-button.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SideMenuButtonComponent {
  label = input.required<string>();
  icon = input.required<string>();
  path = input.required<string>();
}
