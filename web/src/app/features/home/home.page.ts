import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
})
export class HomePageComponent {}

  logout() {
    this.#authMediator.logout();
    this.#authStore.reset();
  }
}
