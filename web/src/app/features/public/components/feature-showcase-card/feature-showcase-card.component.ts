import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'tr-feature-showcase-card',
  imports: [NgIcon, NgClass],
  templateUrl: './feature-showcase-card.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class FeatureShowcaseCardComponent {
  title = input.required<string>();
  color = input.required<string>();
  icon = input.required<string>();
  description = input.required<string>();
}
