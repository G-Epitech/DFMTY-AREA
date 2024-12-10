import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'tr-action-card',
  imports: [NgOptimizedImage],
  templateUrl: './action-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ActionCardComponent {
  label = input.required<string>();
}
