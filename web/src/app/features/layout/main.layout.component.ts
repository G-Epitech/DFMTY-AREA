import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { SideMenuButtonComponent } from '@features/layout/components/side-menu-button/side-menu-button.component';
import { SideMenuProfileComponent } from '@features/layout/components/side-menu-profile/side-menu-profile.component';

@Component({
  selector: 'tr-layout',
  imports: [
    RouterOutlet,
    NgOptimizedImage,
    SideMenuButtonComponent,
    SideMenuProfileComponent,
  ],
  templateUrl: './main.layout.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class MainLayoutComponent {}
