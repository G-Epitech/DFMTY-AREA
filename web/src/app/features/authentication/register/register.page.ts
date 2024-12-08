import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
} from '@angular/core';
import { AppRouter } from '@app/app.router';
import { AuthStore } from '@app/store';
import { RouterLink } from '@angular/router';
import { RegisterFormComponent } from '@features/authentication/register/components/register-form/register-form.component';

@Component({
  selector: 'tr-register',
  imports: [RouterLink, RegisterFormComponent],
  templateUrl: './register.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterPageComponent {
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
