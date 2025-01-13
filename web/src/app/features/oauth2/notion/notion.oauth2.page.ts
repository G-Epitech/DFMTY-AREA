import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import {
  Oauth2BaseComponent
} from '@features/oauth2/components/oauth2-base-page/oauth2-base.component';

@Component({
  selector: 'tr-notion.oauth2',
  imports: [Oauth2BaseComponent],
  templateUrl: './notion.oauth2.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class NotionOauth2PageComponent {
  loading = signal<boolean>(false);
  success = signal<boolean>(false);
}
