import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LabelDirective } from '@triggo-ui/label';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputDirective } from '@triggo-ui/input';
import { Subject, takeUntil, tap } from 'rxjs';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrFormPasswordComponent } from '@triggo-ui/form';

@Component({
  selector: 'tr-login-form',
  imports: [
    FormsModule,
    LabelDirective,
    ReactiveFormsModule,
    TrButtonDirective,
    TrInputDirective,
    TrFormPasswordComponent,
  ],
  templateUrl: './login-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LoginFormComponent implements OnDestroy {
  readonly #authMediator = inject(AuthMediator);
  readonly #store = inject(AuthStore);
  private destroy$ = new Subject<void>();

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  onLogin() {
    if (this.loginForm.invalid) {
      return;
    }
    const { email, password } = this.loginForm.value;
    if (!email || !password) {
      return;
    }
    this.#authMediator
      .login(email, password)
      .pipe(
        takeUntil(this.destroy$),
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
