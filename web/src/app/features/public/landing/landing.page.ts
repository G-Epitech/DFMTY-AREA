import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'tr-landing',
  imports: [RouterLink, RouterLinkActive, TrButtonDirective, NgOptimizedImage],
  templateUrl: './landing.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LandingPageComponent {}
