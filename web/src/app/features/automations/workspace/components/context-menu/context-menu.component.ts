import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { ContextMenuTabsComponent } from '@features/automations/workspace/components/context-menu-tabs/context-menu-tabs.component';

@Component({
  selector: 'tr-context-menu',
  imports: [ContextMenuTabsComponent],
  templateUrl: './context-menu.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ContextMenuComponent {
  selectedTab = signal<number>(1);
}
