import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-public-navbar',
  imports: [
    NgOptimizedImage,
    RouterLink,
    RouterLinkActive,
    TrButtonDirective,
    RouterOutlet,
  ],
  templateUrl: './public-layout.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class PublicLayoutComponent {}
