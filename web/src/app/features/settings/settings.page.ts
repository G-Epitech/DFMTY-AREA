import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-settings',
  imports: [TrButtonDirective],
  templateUrl: './settings.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SettingsPageComponent {
  readonly #authMediator = inject(AuthMediator);
  readonly #authStore = inject(AuthStore);

  logout() {
    this.#authMediator.logout();
    this.#authStore.reset();
  }
}
