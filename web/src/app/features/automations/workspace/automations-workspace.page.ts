import { ChangeDetectionStrategy, Component, computed } from '@angular/core';
import { AutomationModel } from '@models/automation.model';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';
import { AsyncPipe, NgStyle } from '@angular/common';
import { ContextMenuComponent } from '@features/automations/workspace/components/context-menu/context-menu.component';
import { Observable, of } from 'rxjs';
import {
  ActionCardComponent
} from '@features/automations/workspace/components/action-card/action-card.component';

@Component({
  selector: 'tr-automations-workspace',
  imports: [
    NgIcon,
    NgStyle,
    ContextMenuComponent,
    AsyncPipe,
    ActionCardComponent,
  ],
  templateUrl: './automations-workspace.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsWorkspacePageComponent {
  automation: AutomationModel = new AutomationModel(
    '1',
    'Reply “Feur” to “Quoi”',
    'Description 1',
    true,
    new Date(),
    '#EE883A',
    'chat-bubble-bottom-center-text'
  );

  icon = iconName(this.automation.iconName);

  actions: Observable<string[]>;

  constructor() {
    this.actions = of(['Message received in channel', 'Reply with message']);
  }
}
