import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

export interface IntegrationAvailableCardProps {
  logoAssetName: string;
  name: string;
  description: string;
  features: string[];
}

@Component({
  selector: 'tr-integration-available-card',
  imports: [NgOptimizedImage],
  templateUrl: './integration-available-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class IntegrationAvailableCardComponent {
  props = input.required<IntegrationAvailableCardProps>();
}
