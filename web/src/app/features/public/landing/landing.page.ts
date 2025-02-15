import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FeatureShowcaseCardComponent } from '@features/public/components/feature-showcase-card/feature-showcase-card.component';

@Component({
  selector: 'tr-landing',
  imports: [
    TrButtonDirective,
    NgOptimizedImage,
    RouterLink,
    FeatureShowcaseCardComponent,
  ],
  templateUrl: './landing.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LandingPageComponent {}
