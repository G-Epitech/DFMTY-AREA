import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-integrations',
  imports: [TrButtonDirective],
  templateUrl: './integrations.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationsPageComponent {}
