import {ChangeDetectionStrategy, Component} from '@angular/core';

@Component({
  selector: 'tr-home',
  imports: [],
  templateUrl: './home.page.html',
  standalone: true,
  styleUrl: './home.page.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomePage {

}
