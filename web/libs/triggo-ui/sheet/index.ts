import { NgModule } from '@angular/core';

import { TrSheetCloseDirective } from './tr-sheet-close.directive';
import { TrSheetContentComponent } from './tr-sheet-content.component';
import { TrSheetDescriptionDirective } from './tr-sheet-description.directive';
import { TrSheetFooterComponent } from './tr-sheet-footer.component';
import { TrSheetHeaderComponent } from './tr-sheet-header.component';
import { TrSheetOverlayDirective } from './tr-sheet-overlay.directive';
import { TrSheetTitleDirective } from './tr-sheet-title.directive';
import { TrSheetComponent } from './tr-sheet.component';

export * from './tr-sheet-close.directive';
export * from './tr-sheet-content.component';
export * from './tr-sheet-description.directive';
export * from './tr-sheet-footer.component';
export * from './tr-sheet-header.component';
export * from './tr-sheet-overlay.directive';
export * from './tr-sheet-title.directive';
export * from './tr-sheet.component';

export const TrSheetImports = [
	TrSheetComponent,
	TrSheetCloseDirective,
	TrSheetContentComponent,
	TrSheetDescriptionDirective,
	TrSheetFooterComponent,
	TrSheetHeaderComponent,
	TrSheetOverlayDirective,
	TrSheetTitleDirective,
] as const;

@NgModule({
	imports: [...TrSheetImports],
	exports: [...TrSheetImports],
})
export class TrSheetModule {}
