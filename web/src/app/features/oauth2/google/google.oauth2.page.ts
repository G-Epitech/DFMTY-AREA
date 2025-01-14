import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GoogleMediator } from '@mediators/google.mediator';
import { finalize } from 'rxjs/operators';
import { AuthStore } from '@app/store';
import { Subject, takeUntil } from 'rxjs';
import { AppRouter } from '@app/app.router';
import { ToastrService } from 'ngx-toastr';
import { Oauth2BaseComponent } from '@features/oauth2/components/oauth2-base-page/oauth2-base.component';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-google.oauth2.page',
  imports: [Oauth2BaseComponent, TrButtonDirective],
  templateUrl: './google.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GoogleOauth2PageComponent implements OnInit, OnDestroy {
  readonly #googleMediator = inject(GoogleMediator);
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);
  readonly #toastr = inject(ToastrService);

  readonly #destroyRef = new Subject<void>();

  loading = signal<boolean>(false);
  success = signal<boolean>(false);

  constructor(private route: ActivatedRoute) {
    effect(() => {
      if (this.#store.isAuthenticated()) {
        this.#appRouter.redirectToHome();
      }
    });
  }

  ngOnInit() {
    this.loading.set(true);
    this.route.queryParams.subscribe(params => {
      const code: string | null = params['code'];

      if (code) {
        this.#linkGoogeAccount(code);
      } else {
        this.success.set(false);
      }
    });
  }

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  #linkGoogeAccount(code: string): void {
    this.#googleMediator
      .sendCode(code)
      .pipe(
        finalize(() => this.loading.set(false)),
        takeUntil(this.#destroyRef)
      )
      .subscribe({
        next: () => {
          this.success.set(true);
          this.#store.me();
          this.#toastr.success(
            'Google account linked successfully',
            'Login Success'
          );
        },
        error: () => {
          this.success.set(false);
          this.#toastr.error('Google account link failed', 'Login Failed');
        },
      });
  }

  goToLogin(): void {
    this.#appRouter.redirectToLogin();
  }
}
