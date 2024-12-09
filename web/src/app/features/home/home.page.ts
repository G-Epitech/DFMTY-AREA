import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [TrButtonDirective],
})
export class HomePageComponent {
  openDiscordOAuthPage() {
    const url = `${window.location.origin}/oauth2/discord`;
    const newWindow = window.open(url, '_blank');
    if (newWindow) {
      newWindow.opener = window;
    }
  }
}
