import { ChangeDetectionStrategy, Component, computed } from '@angular/core';
import { AutomationModel } from '@models/automation.model';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  ContextMenuComponent
} from '@features/automations/workspace/components/context-menu/context-menu.component';

@Component({
  selector: 'tr-automations-workspace',
  imports: [NgIcon, NgStyle, ContextMenuComponent],
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
}
