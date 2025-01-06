import { InjectionToken } from '@angular/core';
import { AutomationModel } from '@models/automation';
import { PagerCacheService } from '@common/cache/pager-cache.service';
import { IntegrationModel } from '@models/integration';

export const AUTOMATION_CACHE_SERVICE = new InjectionToken<
  PagerCacheService<AutomationModel>
>('AutomationCacheService');

export const INTEGRATION_CACHE_SERVICE = new InjectionToken<
  PagerCacheService<IntegrationModel>
>('UserCacheService');
