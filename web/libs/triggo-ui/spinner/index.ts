import { NgModule } from '@angular/core';
import { TrSpinnerComponent } from './tr-spinner.component';

export * from './tr-spinner.component';

@NgModule({
  imports: [TrSpinnerComponent],
  exports: [TrSpinnerComponent],
})
export class HlmSpinnerModule {}
