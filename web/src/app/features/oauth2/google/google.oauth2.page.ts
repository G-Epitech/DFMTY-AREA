import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'tr-google.oauth2.page',
  imports: [],
  templateUrl: './google.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GoogleOauth2PageComponent {
  loading = signal<boolean>(false);
  success = signal<boolean>(false);

  constructor(private route: ActivatedRoute) {}
}
