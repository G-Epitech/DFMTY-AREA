import { NgModule } from '@angular/core';

import { TrDialogCloseDirective } from './tr-dialog-close.directive';
import { TrDialogContentComponent } from './tr-dialog-content.component';
import { TrDialogDescriptionDirective } from './tr-dialog-description.directive';
import { TrDialogFooterComponent } from './tr-dialog-footer.component';
import { TrDialogHeaderComponent } from './tr-dialog-header.component';
import { TrDialogOverlayDirective } from './tr-dialog-overlay.directive';
import { TrDialogTitleDirective } from './tr-dialog-title.directive';
import { TrDialogComponent } from './tr-dialog.component';

export * from './tr-dialog-close.directive';
export * from './tr-dialog-content.component';
export * from './tr-dialog-description.directive';
export * from './tr-dialog-footer.component';
export * from './tr-dialog-header.component';
export * from './tr-dialog-overlay.directive';
export * from './tr-dialog-title.directive';
export * from './tr-dialog.component';
export * from './tr-dialog.service';

export const TrDialogImports = [
  TrDialogComponent,
  TrDialogCloseDirective,
  TrDialogContentComponent,
  TrDialogDescriptionDirective,
  TrDialogFooterComponent,
  TrDialogHeaderComponent,
  TrDialogOverlayDirective,
  TrDialogTitleDirective,
] as const;

@NgModule({
  imports: [...TrDialogImports],
  exports: [...TrDialogImports],
})
export class TrDialogModule {}
