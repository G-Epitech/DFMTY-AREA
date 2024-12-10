import {
  ChangeDetectionStrategy,
  Component,
  computed,
  signal,
} from '@angular/core';
import { ContextMenuTabsComponent } from '@features/automations/workspace/components/context-menu-tabs/context-menu-tabs.component';
import { NgOptimizedImage } from '@angular/common';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-context-menu',
  imports: [ContextMenuTabsComponent, NgOptimizedImage, NgIcon],
  templateUrl: './context-menu.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ContextMenuComponent {
  selectedTab = signal<number>(1);

  isActionTabSelected = computed(() => this.selectedTab() === 1);

  possibleActions = ['Send message to channel', 'React to message'];
}
