import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [TrButtonDirective],
})
export class HomePageComponent {
  readonly #authMediator = inject(AuthMediator);
  readonly #authStore = inject(AuthStore);

  logout() {
    this.#authMediator.logout();
    this.#authStore.reset();
  }
}
