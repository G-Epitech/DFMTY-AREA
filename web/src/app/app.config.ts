import {
  ApplicationConfig,
  enableProdMode,
  inject,
  provideAppInitializer,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './interceptors';
import { AppService } from './app.service';
import { environment } from '../environments/environment';
import { provideIcons } from '@ng-icons/core';
import {
  heroHome,
  heroBolt,
  heroLink,
  heroExclamationTriangle,
  heroCheckCircle,
  heroCheck,
  heroArrowLeft,
  heroArrowRight,
} from '@ng-icons/heroicons/outline';
import {
  heroSparklesSolid,
  heroChatBubbleBottomCenterTextSolid,
} from '@ng-icons/heroicons/solid';
import { provideToastr } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';

if (environment.production) {
  enableProdMode();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideAppInitializer(() => {
      void inject(AppService).appInit();
    }),
    {
      provide: 'BASE_URL',
      useValue: environment.apiUrl,
    },
    provideIcons({
      heroHome,
      heroLink,
      heroBolt,
      heroCheckCircle,
      heroExclamationTriangle,
      heroArrowRight,
      heroArrowLeft,
      heroCheck,
      heroSparklesSolid,
      heroChatBubbleBottomCenterTextSolid,
    }),
    provideAnimations(),
    provideToastr({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
};
