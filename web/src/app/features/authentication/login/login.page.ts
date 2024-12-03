import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
} from '@angular/core';
import { AuthStore } from '@app/store';
import { AppRouter } from '@app/app.router';
import { TrInputDirective } from '@triggo-ui/input';
import { TrButtonDirective } from '@triggo-ui/button';
import { LabelDirective } from '@triggo-ui/label';

@Component({
  selector: 'tr-login',
  imports: [
    TrInputDirective,
    TrButtonDirective,
    LabelDirective,
  ],
  templateUrl: './login.page.html',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);

  redirectToHome: EffectRef;

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }
}
