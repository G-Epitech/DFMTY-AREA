import { NgModule } from '@angular/core';
import { provideIcons as provideIconsImport } from '@ng-icons/core';
import { TrIconComponent } from './tr-icon.component';

export * from './tr-icon.component';

export const provideIcons = provideIconsImport;

@NgModule({
	imports: [TrIconComponent],
	exports: [TrIconComponent],
})
export class HlmIconModule {}
