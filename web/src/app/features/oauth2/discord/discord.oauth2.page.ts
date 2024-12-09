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

@Component({
  selector: 'tr-oauth2-discord',
  templateUrl: './discord.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [TrSpinnerComponent, NgOptimizedImage, NgIcon, TrButtonDirective],
})
export class DiscordOAuth2PageComponent implements OnInit {
  readonly #discordRepository = inject(DiscordRepository);

  loading = signal<boolean>(false);
  error = signal<boolean>(false);
  success = signal<boolean>(false);

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loading.set(true);
    this.route.queryParams.subscribe(params => {
      const accessToken: string | null = params['accessToken'];
      const code: string | null = params['code'];
      const state: string | null = params['state'];

      if (accessToken) {
        localStorage.setItem('accessToken', accessToken);
      }

      if (code && state) {
        this.#linkDiscordAccount(code, state);
      } else {
        this.#intializeDiscordAuth();
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
          this.error.set(false);
        },
        error: () => {
          this.error.set(true);
        },
      });
  }

  #intializeDiscordAuth(): void {
    this.#discordRepository
      .getUri()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: uri => {
          if (!uri) {
            this.error.set(true);
            return;
          }
          window.location.href = uri;
        },
        error: () => {
          this.error.set(true);
        },
      });
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
