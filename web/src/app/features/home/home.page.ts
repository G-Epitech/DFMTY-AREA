import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { finalize } from 'rxjs/operators';
import { DiscordRepository } from '@repositories/integrations';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [TrButtonDirective],
})
export class HomePageComponent {
  readonly #discordRepository = inject(DiscordRepository);

  loading = signal<boolean>(false);
  error = signal<boolean>(false);

  openDiscordOAuthPage() {
    const url = `${window.location.origin}/oauth2/discord`;
    this.#discordRepository
      .getUri()
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: uri => {
          if (!uri) {
            this.error.set(true);
            return;
          }
          const newWindow = window.open(`${uri}`, '_blank');
          if (newWindow) {
            newWindow.opener = window;
          }
        },
        error: () => {
          this.error.set(true);
        },
      });
  }
}
