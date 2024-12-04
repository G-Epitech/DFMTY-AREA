import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
  OnDestroy,
} from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { Subject, tap } from 'rxjs';
import { AppRouter } from '@app/app.router';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';
import { FormsModule } from '@angular/forms';
import { LabelDirective } from '@triggo-ui/label';
import { TrInputDirective } from '@triggo-ui/input';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'tr-register',
  imports: [
    TrButtonDirective,
    FormsModule,
    LabelDirective,
    TrInputDirective,
    RouterLink,
  ],
  templateUrl: './register.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterPageComponent implements OnDestroy {
  readonly #authMediator = inject(AuthMediator);
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);
  private destroy$ = new Subject<void>();

  redirectToHome: EffectRef;

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }

  onRegister(): void {
    this.#authMediator
      .register('example@gmail.com', '12345678', 'dragos', 'suceveanu')
      .pipe(
        tap({
          next: () => this.#store.me(),
          error: error => console.error('Failed to register user', error),
        })
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
