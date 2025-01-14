import { ChangeDetectionStrategy, Component } from '@angular/core';
import { GoogleAuthButtonComponent } from '@components/google-auth-button/google-auth-button.component';

@Component({
  selector: 'tr-oauth-section',
  imports: [GoogleAuthButtonComponent],
  templateUrl: './oauth-section.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OauthSectionComponent {}
