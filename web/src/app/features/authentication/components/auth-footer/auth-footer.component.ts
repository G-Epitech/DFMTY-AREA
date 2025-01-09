import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'tr-auth-footer',
  imports: [RouterLink],
  templateUrl: './auth-footer.component.html',
  styles: [],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AuthFooterComponent {}
