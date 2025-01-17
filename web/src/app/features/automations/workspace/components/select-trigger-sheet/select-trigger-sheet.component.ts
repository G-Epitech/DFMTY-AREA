import {
  ChangeDetectionStrategy,
  Component,
  computed,
  effect,
  inject,
  input,
} from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/cards/trigger-card/trigger-card.component';
import {
  AutomationSchemaModel,
  AutomationSchemaTrigger,
  TriggerShortModel,
} from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
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
  ],
  templateUrl: './select-trigger-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SelectTriggerSheetService],
})
export class SelectTriggerSheetComponent {
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;

  readonly service = inject(SelectTriggerSheetService);
  protected readonly TriggerSelectionStep = TriggerSelectionStep;
  protected readonly state = this.service.state;

  trigger = input.required<TriggerShortModel | null>();

  title = computed(() => {
    const step = this.service.state().selectionStep;
    const titles: Record<TriggerSelectionStep, string> = {
      [TriggerSelectionStep.MAIN]: 'Edit Trigger',
      [TriggerSelectionStep.INTEGRATION]: 'Select Integration',
      [TriggerSelectionStep.LINKED_INTEGRATION]: 'Select Linked Integration',
      [TriggerSelectionStep.TRIGGER]: 'Select Trigger',
    };
    return titles[step];
  });

  description = computed(() => {
    const step = this.service.state().selectionStep;
    const descriptions: Record<TriggerSelectionStep, string> = {
      [TriggerSelectionStep.MAIN]: 'Edit the trigger for this automation',
      [TriggerSelectionStep.INTEGRATION]: 'Choose an integration to use',
      [TriggerSelectionStep.LINKED_INTEGRATION]: 'Select a linked account',
      [TriggerSelectionStep.TRIGGER]: 'Choose a trigger event',
    };
    return descriptions[step];
  });

  constructor() {
    effect(() => {
      const schema = this.#schemaStore.getSchema();
      if (schema) {
        this.schema = schema;
        this.service.initialize(this.trigger(), schema);
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
    this.service.selectTrigger(trigger);
  }

  protected onBack(): void {
    this.service.back();
  }
}
