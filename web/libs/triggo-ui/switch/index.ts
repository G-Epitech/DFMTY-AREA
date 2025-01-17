import { NgModule } from '@angular/core';

import { TrSwitchThumbDirective } from './tr-switch-thumb.directive';
import { TrSwitchComponent } from './tr-switch.component';

export * from './tr-switch-thumb.directive';
export * from './tr-switch.component';

export const TrSwitchImports = [
  TrSwitchComponent,
  TrSwitchThumbDirective,
] as const;
@NgModule({
  imports: [...TrSwitchImports],
  exports: [...TrSwitchImports],
})
export class TrSwitchModule {}
