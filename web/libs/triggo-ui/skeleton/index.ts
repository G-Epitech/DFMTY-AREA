import { NgModule } from '@angular/core';
import { TrSkeletonComponent } from './tr-skeleton.component';

export * from './tr-skeleton.component';

@NgModule({
	imports: [TrSkeletonComponent],
	exports: [TrSkeletonComponent],
})
export class TrSkeletonModule {}
