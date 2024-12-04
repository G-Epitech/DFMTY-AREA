import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { AuthStore } from '@app/store';
import { NgOptimizedImage, TitleCasePipe } from '@angular/common';

@Component({
  selector: 'tr-side-menu-profile',
  imports: [TrButtonDirective, NgOptimizedImage, TitleCasePipe],
  templateUrl: './side-menu-profile.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SideMenuProfileComponent {
  readonly #store = inject(AuthStore);

  readonly user = this.#store.getUser;
}
