import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
} from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/cards/trigger-card/trigger-card.component';
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
  TriggerParameter,
} from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { AutomationStepSelectionStep } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/integration-selection/integration-selection.component';
import { AvailableIntegrationButtonComponent } from '@components/buttons/available-integration-button/available-integration-button.component';
import { SelectTriggerSheetService } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.service';
import { LinkedIntegrationSelectionComponent } from '@features/automations/workspace/components/linked-integration-selection/linked-integration-selection.component';
import { LinkedIntegrationButtonComponent } from '@components/buttons/linked-integration-button/linked-integration-button.component';
import { TriggerSelectionComponent } from '@features/automations/workspace/components/select-trigger-sheet/trigger-selection/trigger-selection.component';
import { TriggerSelectionButtonComponent } from '@features/automations/workspace/components/select-trigger-sheet/trigger-selection-button/trigger-selection-button.component';
import { AvailableIntegrationType } from '@common/types';
import { IntegrationModel } from '@models/integration';
import { SchemaStore } from '@app/store/schema-store';
import { AutomationParameterListComponent } from '@features/automations/workspace/components/automation-parameter-list/automation-parameter-list.component';
import { AutomationParameterEditComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.component';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';

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
export class SelectTriggerSheetComponent {
  readonly #schemaStore = inject(SchemaStore);
  readonly #workspaceStore = inject(AutomationWorkspaceStore);

  schema: AutomationSchemaModel | null = null;

  readonly service = inject(SelectTriggerSheetService);
  protected readonly TriggerSelectionStep = AutomationStepSelectionStep;
  protected readonly state = this.service.state;

  trigger = this.#workspaceStore.getTrigger;

  constructor() {
    effect(async () => {
      const schema = this.#schemaStore.getSchema();
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

  protected onBack(): void {
    this.service.back();
  }
}
