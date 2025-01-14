import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EffectRef,
  inject,
} from '@angular/core';
import { AuthStore } from '@app/store';
import { AppRouter } from '@app/app.router';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LoginFormComponent } from '@features/authentication/login/components/login-form/login-form.component';
import { NgOptimizedImage } from '@angular/common';
import { AuthFooterComponent } from '@features/authentication/components/auth-footer/auth-footer.component';
import { OauthSectionComponent } from '@features/authentication/components/oauth-section/oauth-section.component';

@Component({
  selector: 'tr-login',
  imports: [
    FormsModule,
    RouterLink,
    LoginFormComponent,
    NgOptimizedImage,
    AuthFooterComponent,
    OauthSectionComponent,
  ],
  templateUrl: './login.page.html',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);

  redirectToHome: EffectRef;

  constructor() {
    this.redirectToHome = effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }
}
