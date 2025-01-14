import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { AuthStore } from '@app/store';
import { NgOptimizedImage, TitleCasePipe } from '@angular/common';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'tr-side-menu-profile',
  imports: [
    TrButtonDirective,
    NgOptimizedImage,
    TitleCasePipe,
    TrSkeletonComponent,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './side-menu-profile.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SideMenuProfileComponent {
  readonly #store = inject(AuthStore);

  readonly user = this.#store.getUser;
}
