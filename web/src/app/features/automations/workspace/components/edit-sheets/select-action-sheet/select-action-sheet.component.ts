import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  input,
} from '@angular/core';
import { AutomationParameterEditComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.component';
import { AutomationParameterListComponent } from '@features/automations/workspace/components/automation-parameter-list/automation-parameter-list.component';
import { AvailableIntegrationButtonComponent } from '@components/buttons/available-integration-button/available-integration-button.component';
import {
  BrnSheetImports,
  BrnSheetTriggerDirective,
} from '@spartan-ng/ui-sheet-brain';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/integration-selection/integration-selection.component';
import { LinkedIntegrationButtonComponent } from '@components/buttons/linked-integration-button/linked-integration-button.component';
import { LinkedIntegrationSelectionComponent } from '@features/automations/workspace/components/linked-integration-selection/linked-integration-selection.component';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import {
  TrSheetComponent,
  TrSheetContentComponent,
  TrSheetDescriptionDirective,
  TrSheetHeaderComponent,
  TrSheetTitleDirective,
} from '@triggo-ui/sheet';
import { TriggerSelectionButtonComponent } from '@features/automations/workspace/components/select-trigger-sheet/trigger-selection-button/trigger-selection-button.component';
import { TriggerSelectionComponent } from '@features/automations/workspace/components/select-trigger-sheet/trigger-selection/trigger-selection.component';
import { EditSheetComponentBase } from '@features/automations/workspace/components/edit-sheets/edit-sheet.component.base';
import { SelectActionSheetService } from '@features/automations/workspace/components/edit-sheets/select-action-sheet/select-action-sheet.service';
import { ActionCardComponent } from '@features/automations/workspace/components/cards/action-card/action-card.component';

@Component({
  selector: 'tr-select-action-sheet',
  imports: [
    AutomationParameterEditComponent,
    AutomationParameterListComponent,
    AvailableIntegrationButtonComponent,
    BrnSheetTriggerDirective,
    IntegrationSelectionComponent,
    LinkedIntegrationButtonComponent,
    LinkedIntegrationSelectionComponent,
    NgIcon,
    TrButtonDirective,
    TrSheetComponent,
    TrSheetContentComponent,
    TrSheetDescriptionDirective,
    TrSheetHeaderComponent,
    TrSheetTitleDirective,
    TriggerSelectionButtonComponent,
    TriggerSelectionComponent,
    ActionCardComponent,
    BrnSheetImports,
  ],
  templateUrl: './select-action-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class SelectActionSheetComponent extends EditSheetComponentBase {
  readonly service: SelectActionSheetService;

  action = computed(() => {
    if (this.actionIndex()) {
      return this.workspaceStore.getAction(this.actionIndex())();
    } else {
      return null;
    }
  });

  actionIndex = input.required<number>();

  constructor(
    service: SelectActionSheetService = inject(SelectActionSheetService)
  ) {
    super(service);
    this.service = service;
  }
}
