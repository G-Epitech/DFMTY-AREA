import { NgModule } from '@angular/core';
import { TrBadgeDirective } from './tr-badge.directive';

export * from './tr-badge.directive';

@NgModule({
	imports: [TrBadgeDirective],
	exports: [TrBadgeDirective],
})
export class HlmBadgeModule {}
