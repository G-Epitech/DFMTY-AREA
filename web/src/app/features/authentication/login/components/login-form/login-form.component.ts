import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  signal,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { LabelDirective } from '@triggo-ui/label';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputDirective } from '@triggo-ui/input';
import { Subject, takeUntil, tap } from 'rxjs';
import { AuthMediator } from '@mediators/auth.mediator';
import { AuthStore } from '@app/store';
import { TrFormPasswordComponent } from '@triggo-ui/form';
import { TrSpinnerComponent } from '@triggo-ui/spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'tr-login-form',
  imports: [
    FormsModule,
    LabelDirective,
    ReactiveFormsModule,
    TrButtonDirective,
    TrInputDirective,
    TrFormPasswordComponent,
    TrSpinnerComponent,
  ],
  templateUrl: './login-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LoginFormComponent implements OnDestroy {
  readonly #authMediator = inject(AuthMediator);
  readonly #store = inject(AuthStore);
  readonly #toastr = inject(ToastrService);

  private destroy$ = new Subject<void>();

  loginLoading = signal<boolean>(false);

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  onLogin() {
    if (
      this.loginForm.controls.email.value?.trim() === '' ||
      this.loginForm.controls.password.value?.trim() === ''
    ) {
      this.#toastr.error('Email and password are required', 'Login failed');
      return;
    }
    if (this.loginForm.invalid) {
      return;
    }
    const { email, password } = this.loginForm.value;
    if (!email || !password) {
      return;
    }
    this.loginLoading.set(true);
    this.#authMediator
      .login(email, password)
      .pipe(
        takeUntil(this.destroy$),
        tap({
          next: () => this.#store.me(),
          error: () => {
            this.loginLoading.set(false);
            this.#toastr.error('Invalid email or password', 'Login failed');
          },
        })
      )
      .subscribe(() => {
        this.#toastr.success('Logged in successfully', 'Success');
        this.loginLoading.set(false);
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
