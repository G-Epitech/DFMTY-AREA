import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
  OnDestroy,
} from '@angular/core';
import { AuthMediator } from '@mediators/auth.mediator';
import { Subject, takeUntil, tap } from 'rxjs';
import { AppRouter } from '@app/app.router';
import { AuthStore } from '@app/store';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
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
    ReactiveFormsModule,
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

  confirmPasswordValidator = (
    control: AbstractControl
  ): Record<string, boolean> | null => {
    const password = this.registerForm?.get('password');
    const confirmPassword = control;

    if (
      password &&
      confirmPassword &&
      password.value !== confirmPassword.value
    ) {
      return { passwordMismatch: true };
    }
    return null;
  };

  registerForm = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    confirmPassword: new FormControl('', [this.confirmPasswordValidator]),
  });

  getEmailErrorMessage() {
    const emailControl = this.registerForm.controls.email;
    if (emailControl.errors?.['required']) {
      return 'Email is required';
    }
    if (emailControl.errors?.['email']) {
      return 'Invalid email';
    }
    return 'Email is invalid';
  }

  getPasswordErrorMessage() {
    const passwordControl = this.registerForm.controls.password;
    if (passwordControl.errors?.['required']) {
      return 'Password is required';
    }
    if (passwordControl.errors?.['minlength']) {
      return 'Password must be at least 8 characters';
    }
    return 'Password is invalid';
  }

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      return;
    }
    const { email, password, firstName, lastName } = this.registerForm.value;
    if (!email || !password || !firstName || !lastName) {
      return;
    }
    this.#authMediator
      .register(
        email.trim(),
        password.trim(),
        firstName.trim(),
        lastName.trim()
      )
      .pipe(
        takeUntil(this.destroy$),
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
