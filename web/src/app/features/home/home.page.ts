import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'tr-home',
  templateUrl: './home.page.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomePageComponent {}
