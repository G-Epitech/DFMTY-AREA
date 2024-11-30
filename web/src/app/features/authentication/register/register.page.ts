import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { AuthMediator } from '@mediators/auth.mediator';
import { Observable, tap } from 'rxjs';
import { TokensModel } from '@models/tokens.model';
import { AppRouter } from '@app/app.router';

@Component({
  selector: 'tr-register',
  imports: [AsyncPipe],
  templateUrl: './register.page.html',
  standalone: true,
  styleUrl: './register.page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterPage {
  readonly #authMediator = inject(AuthMediator);
  registrationResult$: Observable<TokensModel> | undefined;
  readonly #appRouter = inject(AppRouter);

  onRegister(): void {
    this.registrationResult$ = this.#authMediator.register(
      'email',
      'password',
      'firstName',
      'lastName'
    );
    this.registrationResult$.pipe(
      tap({
        next: () => void this.#appRouter.redirectToHome(),
        error: () => console.error('Failed to register user'),
      })
    );
  }
}
