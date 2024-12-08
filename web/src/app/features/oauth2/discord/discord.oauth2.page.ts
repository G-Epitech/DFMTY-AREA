import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';
import { DiscordRepository } from '@repositories/integrations';

@Component({
  selector: 'tr-oauth2-discord',
  imports: [],
  templateUrl: './discord.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DiscordOAuth2PageComponent implements OnInit {
  readonly #discordRepository = inject(DiscordRepository);

  accessToken: string | null = null;
  code: string | null = null;
  state: string | null = null;

  uri: string | null = null;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams
      .pipe(filter(params => params['accessToken'] || params['code'] || params['state']))
      .subscribe(params => {
        console.log("Query params received:", params);

        this.accessToken = params['accessToken'];
        this.code = params['code'];
        this.state = params['state'];

        console.log("Access token:", this.accessToken);

        if (this.accessToken) {
          localStorage.setItem('accessToken', this.accessToken);
        }
      });

    console.log("DiscordOAuth2PageComponent initialized");

    if (this.code && this.state) {
      this.#discordRepository.link({ code: this.code, state: this.state }).subscribe();
    } else {
      this.#discordRepository.getUri().subscribe(uri => {
        console.log("Discord URI received:", uri);
        this.uri = uri;
        window.location.href = uri;
      });
    }

  }
}
