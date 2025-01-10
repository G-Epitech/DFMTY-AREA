import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { DiscordRepository } from '@repositories/integrations';
import { TrSpinnerComponent } from '@triggo-ui/spinner';
import { NgOptimizedImage } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  Oauth2BaseComponent
} from '@features/oauth2/components/oauth2-base-page/oauth2-base.component';

@Component({
  selector: 'tr-oauth2-discord',
  templateUrl: './discord.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    TrSpinnerComponent,
    NgOptimizedImage,
    NgIcon,
    TrButtonDirective,
    Oauth2BaseComponent,
  ],
})
export class DiscordOAuth2PageComponent implements OnInit {
  readonly #discordRepository = inject(DiscordRepository);

  loading = signal<boolean>(false);
  success = signal<boolean>(false);
  countdown = signal<number>(5);

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loading.set(true);
    this.route.queryParams.subscribe(params => {
      const code: string | null = params['code'];
      const state: string | null = params['state'];

      if (code && state) {
        this.#linkDiscordAccount(code, state);
      } else {
        this.success.set(false);
      }
    });
  }

  #linkDiscordAccount(code: string, state: string): void {
    this.#discordRepository
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
