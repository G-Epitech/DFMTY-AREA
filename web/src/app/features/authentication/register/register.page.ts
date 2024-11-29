import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {AsyncPipe} from '@angular/common';
import {AuthMediator} from '@mediators/auth.mediator';
import {Observable} from 'rxjs';
import {TokensModel} from '@models/tokens.model';

@Component({
  selector: 'tr-register',
  imports: [
    AsyncPipe
  ],
  templateUrl: './register.page.html',
  standalone: true,
  styleUrl: './register.page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterPage {
  readonly #authMediator = inject(AuthMediator);
  registrationResult$: Observable<TokensModel> | undefined;

  onRegister(): void {
    this.registrationResult$ = this.#authMediator.register('email', 'password', 'firstName', 'lastName');
  }
}
