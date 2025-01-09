import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-oauth-section',
  imports: [NgOptimizedImage, TrButtonDirective],
  templateUrl: './oauth-section.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OauthSectionComponent {}
