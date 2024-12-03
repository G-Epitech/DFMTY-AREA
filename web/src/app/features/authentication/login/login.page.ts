import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
  OnDestroy,
} from '@angular/core';
import { AuthStore } from '@app/store';
import { AppRouter } from '@app/app.router';
import { TrInputDirective } from '@triggo-ui/input';
import { TrButtonDirective } from '@triggo-ui/button';
import { LabelDirective } from '@triggo-ui/label';
import { AuthMediator } from '@mediators/auth.mediator';
import { Subject, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'tr-login',
  imports: [TrInputDirective, TrButtonDirective, LabelDirective, FormsModule],
  templateUrl: './login.page.html',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent implements OnDestroy {
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);
  readonly #authMediator = inject(AuthMediator);

  private destroy$ = new Subject<void>();

  redirectToHome: EffectRef;

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }

  onLogin() {
    this.#authMediator
      .login('example@gmail.com', '12345678')
      .pipe(
        tap({
          next: () => this.#store.me(),
          error: error => console.error('Failed to login user', error),
        })
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
