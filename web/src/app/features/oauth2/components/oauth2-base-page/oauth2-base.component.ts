import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgOptimizedImage } from '@angular/common';
import { TrSpinnerComponent } from '@triggo-ui/spinner';

@Component({
  selector: 'tr-oauth2-base-page',
  imports: [NgIcon, NgOptimizedImage, TrSpinnerComponent],
  templateUrl: './oauth2-base.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class Oauth2BaseComponent {
  name = input.required<string>();
  loading = input.required<boolean>();
  success = input.required<boolean>();
}
