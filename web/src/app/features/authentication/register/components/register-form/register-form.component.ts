import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  signal,
} from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrFormPasswordComponent } from '@triggo-ui/form';
import { TrInputDirective } from '@triggo-ui/input';
import { Subject, takeUntil, tap } from 'rxjs';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrSpinnerComponent } from '@triggo-ui/spinner';
import { ToastrService } from 'ngx-toastr';
import { RegisterFormLabelComponent } from '@features/authentication/register/components/register-form-label/register-form-label.component';

@Component({
  selector: 'tr-register-form',
  imports: [
    FormsModule,
    TrButtonDirective,
    TrFormPasswordComponent,
    TrInputDirective,
    ReactiveFormsModule,
    TrSpinnerComponent,
    RegisterFormLabelComponent,
  ],
  templateUrl: './register-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class RegisterFormComponent implements OnDestroy {
  readonly #authMediator = inject(AuthMediator);
  private destroy$ = new Subject<void>();
  readonly #store = inject(AuthStore);
  readonly #toastr = inject(ToastrService);

  registerLoading = signal<boolean>(false);

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

  registerForm = new FormGroup(
    {
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
      ]),
      confirmPassword: new FormControl('', [this.confirmPasswordValidator]),
    },
    { updateOn: 'submit' }
  );

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSubmit(): void {
    this.registerForm.markAllAsTouched();
    this.registerForm.updateValueAndValidity();
    if (this.registerForm.invalid) {
      return;
    }
    const { email, password, firstName, lastName } = this.registerForm.value;
    if (!email || !password || !firstName || !lastName) {
      return;
    }
    this.registerLoading.set(true);
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
          error: error => {
            this.registerLoading.set(false);
            this.#toastr.error(error.error.title || 'Failed to register');
          },
        })
      )
      .subscribe(() => {
        this.registerLoading.set(false);
        this.#toastr.success('Registration successful');
      });
  }
}
