import {
  ChangeDetectionStrategy,
  Component,
  inject,
  Signal,
} from '@angular/core';
import { IconBoxComponent } from '@components/icon-box/icon-box.component';
import { AutomationTemplateCardComponent } from '@features/home/automation-template-card/automation-template-card.component';
import { AuthStore } from '@app/store';
import { UserModel } from '@models/user.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [IconBoxComponent, AutomationTemplateCardComponent, RouterLink],
})
export class HomePageComponent {
  readonly #authStore = inject(AuthStore);

  user: UserModel | null | undefined = this.#authStore.getUser();
}
