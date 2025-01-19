import { NgModule } from '@angular/core';

import { TrTabsContentDirective } from './tr-tabs-content.directive';
import { TrTabsListComponent } from './tr-tabs-list.component';
import { TrTabsPaginatedListComponent } from './tr-tabs-paginated-list.component';
import { TrTabsTriggerDirective } from './tr-tabs-trigger.directive';
import { TrTabsComponent } from './tr-tabs.component';

export * from './tr-tabs-content.directive';
export * from './tr-tabs-list.component';
export * from './tr-tabs-paginated-list.component';
export * from './tr-tabs-trigger.directive';
export * from './tr-tabs.component';

export const TrTabsImports = [
	TrTabsComponent,
	TrTabsListComponent,
	TrTabsTriggerDirective,
	TrTabsContentDirective,
	TrTabsPaginatedListComponent,
] as const;

@NgModule({
	imports: [...TrTabsImports],
	exports: [...TrTabsImports],
})
export class TrTabsModule {}
