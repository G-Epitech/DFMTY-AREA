import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'tr-integration-available-card',
  imports: [NgOptimizedImage, NgStyle],
  templateUrl: './integration-available-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAvailableCardComponent {
  iconUri = input.required<string>();
  name = input.required<string>();
  color = input.required<string>();
  forceWhite = input.required<boolean>();
}
