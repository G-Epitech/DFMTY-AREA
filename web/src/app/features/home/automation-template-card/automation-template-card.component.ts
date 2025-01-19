import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { IconBoxComponent } from '@components/icon-box/icon-box.component';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-automation-template-card',
  imports: [IconBoxComponent, TrButtonDirective],
  templateUrl: './automation-template-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationTemplateCardComponent {
  icon = input.required<string>();
  color = input.required<string>();
  name = input.required<string>();
  description = input.required<string>();
}
