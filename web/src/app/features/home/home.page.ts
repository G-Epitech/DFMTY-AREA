import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IconBoxComponent } from '@components/icon-box/icon-box.component';
import {
  AutomationTemplateCardComponent
} from '@features/home/automation-template-card/automation-template-card.component';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [IconBoxComponent, AutomationTemplateCardComponent],
})
export class HomePageComponent {}
