import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { DiscordRepository } from '@repositories/integrations';
import { NgIf } from '@angular/common';

@Component({
  selector: 'tr-oauth2-discord',
  templateUrl: './discord.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    NgIf,
  ],
})
export class DiscordOAuth2PageComponent implements OnInit {
  readonly #discordRepository = inject(DiscordRepository);

  accessToken: string | null = null;
  code: string | null = null;
  state: string | null = null;
  uri: string | null = null;
  loading: boolean = false;
  error: boolean = false;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loading = true;
    this.route.queryParams
      .pipe(filter(params => params['accessToken'] || params['code'] || params['state']))
      .subscribe(params => {
        console.log("Query params received:", params);

        this.accessToken = params['accessToken'];
        this.code = params['code'];
        this.state = params['state'];

        if (this.accessToken) {
          localStorage.setItem('accessToken', this.accessToken);
        }

        if (this.code && this.state) {
          this.#discordRepository.link({ code: this.code, state: this.state })
            .pipe(
              catchError(err => {
                this.error = true;
                this.loading = false;
                return of(null);
              })
            )
            .subscribe(() => {
              this.loading = false;
            });
        } else {
          this.#discordRepository.getUri()
            .pipe(
              catchError(err => {
                this.error = true;
                this.loading = false;
                return of(null);
              })
            )
            .subscribe(uri => {
              if (!uri) {
                this.error = true;
                this.loading = false;
                return;
              }
              this.uri = uri;
              this.loading = false;
              window.location.href = uri;
            });
        }
      });

    console.log("DiscordOAuth2PageComponent initialized");
  }

  autoKill(): void {
    window.close();
  }
}
