import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgOptimizedImage, NgStyle } from '@angular/common';

@Component({
  selector: 'tr-available-integration-list-card',
  imports: [TrButtonDirective, NgOptimizedImage, NgStyle],
  templateUrl: './available-integration-list-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AvailableIntegrationListCardComponent {
  iconUri = input.required<string>();
  name = input.required<string>();
  color = input.required<string>();
  forceWhite = input.required<boolean>();
}
