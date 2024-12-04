import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';
import {
  SideMenuButtonComponent
} from '@features/layout/components/side-menu-button/side-menu-button.component';

@Component({
  selector: 'tr-layout',
  imports: [
    RouterOutlet,
    NgOptimizedImage,
    TrButtonDirective,
    NgIcon,
    SideMenuButtonComponent,
  ],
  templateUrl: './main.layout.component.html',
  styleUrl: './main.layout.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class MainLayoutComponent {}
