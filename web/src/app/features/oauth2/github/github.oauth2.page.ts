import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { GithubMediator, NotionMediator } from '@mediators/integrations';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import {
  Oauth2BaseComponent
} from '@features/oauth2/components/oauth2-base-page/oauth2-base.component';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-github.oauth2.page',
  imports: [Oauth2BaseComponent, TrButtonDirective],
  templateUrl: './github.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GithubOauth2PageComponent implements OnInit {
  readonly #mediator = inject(GithubMediator);

  loading = signal<boolean>(false);
  success = signal<boolean>(false);
  countdown = signal<number>(5);

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.loading.set(true);
    this.route.queryParams.subscribe(params => {
      const code: string | null = params['code'];
      const state: string | null = params['state'];

      if (code && state) {
        this.#linkGithubAccount(code, state);
      } else {
        this.success.set(false);
      }
    });
  }

  #linkGithubAccount(code: string, state: string) {
    this.#mediator
      .link(state, code)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: () => {
          this.success.set(true);
          this.startCountdown();
        },
        error: () => {
          this.success.set(false);
          this.loading.set(false);
        },
      });
  }

  startCountdown(): void {
    let count = 5;
    const interval = setInterval(() => {
      count -= 1;
      this.countdown.set(count);
      if (count === 0) {
        clearInterval(interval);
        this.handleCloseWindow();
      }
    }, 1000);
  }

  handleCloseWindow(): void {
    if (window.opener) {
      this.closeWindow();
    } else {
      window.location.href = '/';
    }
  }

  closeWindow(): void {
    window.close();
  }
}
