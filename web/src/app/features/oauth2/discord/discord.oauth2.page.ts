import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
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

  accessToken: string | null = null;
  code: string | null = null;
  state: string | null = null;
  uri: string | null = null;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loading.set(true);
    this.route.queryParams.subscribe(params => {
      this.accessToken = params['accessToken'];
      this.code = params['code'];
      this.state = params['state'];

      if (this.accessToken) {
        localStorage.setItem('accessToken', this.accessToken);
      }

      if (this.code && this.state) {
        this.#discordRepository
          .link({ code: this.code, state: this.state })
          .pipe(
            catchError(() => {
              this.error.set(true);
              this.loading.set(false);
              return of(null);
            })
          )
          .subscribe(() => {
            this.loading.set(false);
            this.closeWindow();
          });
      } else {
        this.#discordRepository
          .getUri()
          .pipe(
            catchError(err => {
              this.error.set(true);
              this.loading.set(false);
              return of(null);
            })
          )
          .subscribe(uri => {
            if (!uri) {
              this.error.set(true);
              this.loading.set(false);
              return;
            }
            this.uri = uri;
            window.location.href = uri;
          });
      }
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
