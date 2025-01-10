import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgOptimizedImage, TitleCasePipe } from '@angular/common';
import { TrSkeletonComponent } from '@triggo-ui/skeleton';

@Component({
  selector: 'tr-settings',
  imports: [
    TrButtonDirective,
    NgOptimizedImage,
    TitleCasePipe,
    TrSkeletonComponent,
  ],
  templateUrl: './settings.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SettingsPageComponent {
  readonly #authMediator = inject(AuthMediator);
  readonly #authStore = inject(AuthStore);

  readonly user = this.#authStore.getUser;

  logout() {
    this.#authMediator.logout();
    this.#authStore.reset();
  }
}
