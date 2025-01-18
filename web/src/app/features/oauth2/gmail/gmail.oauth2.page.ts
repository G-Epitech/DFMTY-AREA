import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { GmailRepository } from '@repositories/integrations';
import { Oauth2BaseComponent } from '@features/oauth2/components/oauth2-base-page/oauth2-base.component';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-oauth2-gmail',
  templateUrl: './gmail.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [Oauth2BaseComponent, TrButtonDirective],
})
export class GmailOAuth2PageComponent implements OnInit {
  readonly #gmailRepository = inject(GmailRepository);

  loading = signal<boolean>(false);
  success = signal<boolean>(false);
  countdown = signal<number>(5);

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loading.set(true);
    debugger;
    this.route.queryParams.subscribe(params => {
      const code: string | null = params['code'];
      const state: string | null = params['state'];

      if (code && state) {
        this.#linkGmailAccount(code, state);
      } else {
        this.success.set(false);
      }
    });
  }

  #linkGmailAccount(code: string, state: string): void {
    this.#gmailRepository
      .link({ code, state })
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
