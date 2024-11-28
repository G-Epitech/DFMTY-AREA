import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {AuthMediator} from '@mediators/auth.mediator';
import {Observable} from 'rxjs';
import {TokensModel} from '@models/tokens.model';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    AsyncPipe
  ]
})
export class AppComponent {
  title = 'triggo-web';

  readonly #authMediator = inject(AuthMediator);
  registrationResult$: Observable<TokensModel> | undefined;

  onRegister(): void {
    console.log("Registering user");
    this.registrationResult$ = this.#authMediator.register('email', 'password', 'firstName', 'lastName');
  }
}
