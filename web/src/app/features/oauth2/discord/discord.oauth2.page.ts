import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'tr-oauth2-discord',
  imports: [],
  templateUrl: './discord.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DiscordOAuth2PageComponent implements OnInit {
  accessToken: string | null = null;
  order: string | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams
      .pipe(filter(params => params['accessToken']))
      .subscribe(params => {
        console.log(params);

        this.accessToken = params['accessToken'];

        console.log(this.accessToken);
      });
  }
}
