import {
  ChangeDetectionStrategy,
  Component,
  computed, effect,
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
import { TriggerSelectionButtonComponent } from '@features/automations/workspace/components/edit-sheets/trigger-selection-button/trigger-selection-button.component';
import { EditSheetComponentBase } from '@features/automations/workspace/components/edit-sheets/edit-sheet.component.base';
import { SelectActionSheetService } from '@features/automations/workspace/components/edit-sheets/select-action-sheet/select-action-sheet.service';
import { ActionCardComponent } from '@features/automations/workspace/components/cards/action-card/action-card.component';
import { ActionParameter, AutomationSchemaAction } from '@models/automation';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import {
  AddStepButtonComponent
} from '@features/automations/workspace/components/add-step-button/add-step-button.component';
import {
  ActionSelectionComponent
} from '@features/automations/workspace/components/edit-sheets/select-action-sheet/action-selection/action-selection.component';

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
    ActionCardComponent,
    BrnSheetImports,
    AddStepButtonComponent,
    ActionSelectionComponent,
  ],
  templateUrl: './select-action-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  providers: [SelectActionSheetService],
})
export class SelectActionSheetComponent extends EditSheetComponentBase {
  readonly service: SelectActionSheetService;

  sheetTrigger = input.required<'action-card' | 'add-button'>();
  actionIndex = input.required<number>();

  action = computed(() => {
    if (this.actionIndex()) {
      return this.workspaceStore.getAction(this.actionIndex())();
    } else {
      return null;
    }
  });

  constructor(
    service: SelectActionSheetService = inject(SelectActionSheetService)
  ) {
    super(service);
    this.service = service;
    effect(async () => {
      const schema = this.schemaStore.getSchema();
      if (schema) {
        console.log('schema val');
        this.schema = schema;
        await this.service.initialize(this.action(), schema);
      }
    });
  }

  protected onParameterEdit(param: ActionParameter): void {
    this.service.goToParameterEdit();
    this.service.selectParameter(param, this.schema);
  }

  protected onIntegrationSelected(integration: AvailableIntegrationType): void {
    this.service.selectIntegration(integration);
  }

  onLinkedIntegrationSelected(event: IntegrationModel) {
    this.service.selectLinkedIntegration(event);
  }

  onActionSelected(action: AutomationSchemaAction) {
    this.service.selectAction(action, this.schema);
  }
}
