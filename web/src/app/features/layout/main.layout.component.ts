import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgOptimizedImage } from '@angular/common';
import { SideMenuButtonComponent } from '@features/layout/components/side-menu-button/side-menu-button.component';
import { SideMenuProfileComponent } from '@features/layout/components/side-menu-profile/side-menu-profile.component';
import { PagerCacheService } from '@common/cache/pager-cache.service';
import {
  AUTOMATION_CACHE_SERVICE,
  INTEGRATION_CACHE_SERVICE,
} from '@common/cache/injection-tokens';
import { AutomationModel } from '@models/automation';
import { IntegrationModel } from '@models/integration';

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
  providers: [
    {
      provide: AUTOMATION_CACHE_SERVICE,
      useFactory: () => new PagerCacheService<AutomationModel>(),
    },
    {
      provide: INTEGRATION_CACHE_SERVICE,
      useFactory: () => new PagerCacheService<IntegrationModel>(),
    },
  ],
})
export class MainLayoutComponent {}
