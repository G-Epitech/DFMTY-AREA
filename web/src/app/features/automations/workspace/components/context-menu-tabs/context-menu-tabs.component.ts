import {
  ChangeDetectionStrategy,
  Component,
  input,
  output,
  signal,
} from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgClass } from '@angular/common';

@Component({
  selector: 'tr-context-menu-tabs',
  imports: [NgIcon, TrButtonDirective, NgClass],
  templateUrl: './context-menu-tabs.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class ContextMenuTabsComponent {
  selectedTab = input.required<number>();

  tabChange = output<number>();

  selectTab(tab: number) {
    this.tabChange.emit(tab);
  }
}
