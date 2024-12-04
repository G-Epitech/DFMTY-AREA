import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'tr-layout',
  imports: [RouterOutlet],
  templateUrl: './main.layout.component.html',
  styleUrl: './main.layout.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class MainLayoutComponent {}
