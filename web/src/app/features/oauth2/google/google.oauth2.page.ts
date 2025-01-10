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

@Component({
  selector: 'tr-google.oauth2.page',
  imports: [],
  templateUrl: './google.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GoogleOauth2PageComponent implements OnInit, OnDestroy {
  readonly #googleMediator = inject(GoogleMediator);
  readonly #store = inject(AuthStore);
  readonly #appRouter = inject(AppRouter);

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
        },
        error: () => {
          this.success.set(false);
        },
      });
  }
}
