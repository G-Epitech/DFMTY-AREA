import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
} from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/cards/trigger-card/trigger-card.component';
import {
  AutomationSchemaTrigger,
  TriggerParameter,
} from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/edit-sheets/integration-selection/integration-selection.component';
import { AvailableIntegrationButtonComponent } from '@components/buttons/available-integration-button/available-integration-button.component';
import { SelectTriggerSheetService } from '@features/automations/workspace/components/edit-sheets/select-trigger-sheet/select-trigger-sheet.service';
import { LinkedIntegrationSelectionComponent } from '@features/automations/workspace/components/edit-sheets/linked-integration-selection/linked-integration-selection.component';
import { LinkedIntegrationButtonComponent } from '@components/buttons/linked-integration-button/linked-integration-button.component';
import { TriggerSelectionComponent } from '@features/automations/workspace/components/edit-sheets/select-trigger-sheet/trigger-selection/trigger-selection.component';
import { TriggerSelectionButtonComponent } from '@features/automations/workspace/components/edit-sheets/automation-step-selection-button/automation-step-selection-button.component';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import { AutomationParameterListComponent } from '@features/automations/workspace/components/edit-sheets/automation-parameter-list/automation-parameter-list.component';
import { AutomationParameterEditComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.component';
import { EditSheetComponentBase } from '@features/automations/workspace/components/edit-sheets/edit-sheet.component.base';

@Component({
  standalone: true,
  selector: 'tr-select-trigger-sheet',
  imports: [
    TrSheetImports,
    TrButtonDirective,
    BrnSheetImports,
    TriggerCardComponent,
    NgIcon,
    IntegrationSelectionComponent,
    AvailableIntegrationButtonComponent,
    LinkedIntegrationSelectionComponent,
    LinkedIntegrationButtonComponent,
    TriggerSelectionComponent,
    TriggerSelectionButtonComponent,
    AutomationParameterListComponent,
    AutomationParameterEditComponent,
  ],
  templateUrl: './select-trigger-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SelectTriggerSheetService],
})
export class SelectTriggerSheetComponent extends EditSheetComponentBase {
  readonly service: SelectTriggerSheetService;

  trigger = this.workspaceStore.getTrigger;

  constructor(service: SelectTriggerSheetService = inject(SelectTriggerSheetService)) {
    super(service);
    this.service = service;
    effect(async () => {
      const schema = this.schemaStore.getSchema();
      if (schema) {
        this.schema = schema;
        await this.service.initialize(this.trigger(), schema);
      }
    });
  }

  protected onIntegrationSelected(integration: AvailableIntegrationType): void {
    this.service.selectIntegration(integration);
  }

  protected onLinkedIntegrationSelected(
    linkedIntegration: IntegrationModel
  ): void {
    this.service.selectLinkedIntegration(linkedIntegration);
  }

  protected onTriggerSelected(trigger: AutomationSchemaTrigger): void {
    this.service.selectTrigger(trigger, this.schema);
  }

  protected onParameterEdit(param: TriggerParameter): void {
    this.service.goToParameterEdit();
    this.service.selectParameter(param, this.schema);
  }
}
