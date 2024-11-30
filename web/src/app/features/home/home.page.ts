import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '../../store';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styleUrl: './home.page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomePage {
  readonly #authMediator = inject(AuthMediator);

  readonly authStore = inject(AuthStore);
  readonly user = this.authStore.getUser;

  logout() {
    this.#authMediator.logout();
  }
}
