import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
} from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { AuthMediator } from '@mediators/auth.mediator';
import { Observable, tap } from 'rxjs';
import { TokensModel } from '@models/tokens.model';
import { AppRouter } from '@app/app.router';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-register',
  imports: [AsyncPipe, TrButtonDirective],
  templateUrl: './register.page.html',
  standalone: true,
  styleUrl: './register.page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterPageComponent {
  readonly #authMediator = inject(AuthMediator);
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);

  registrationResult$: Observable<TokensModel> | undefined;
  redirectToHome: EffectRef;

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }

  onRegister(): void {
    this.registrationResult$ = this.#authMediator
      .register('email', 'password', 'firstName', 'lastName')
      .pipe(
        tap({
          next: () => this.#store.me(),
          error: error => console.error('Failed to register user', error),
        })
      );
  }
}
