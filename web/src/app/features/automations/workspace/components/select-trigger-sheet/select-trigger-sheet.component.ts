import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
} from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/trigger-card/trigger-card.component';
import { TriggerShortModel } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/integration-selection/integration-selection.component';
import { AvailableIntegrationListCardComponent } from '@components/available-integration-list-card/available-integration-list-card.component';
import { SelectTriggerSheetService } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.service';
import {
  LinkedIntegrationSelectionComponent
} from '@features/automations/workspace/components/linked-integration-selection/linked-integration-selection.component';

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
    AvailableIntegrationListCardComponent,
    LinkedIntegrationSelectionComponent,
  ],
  templateUrl: './select-trigger-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SelectTriggerSheetService],
})
export class SelectTriggerSheetComponent {
  service = inject(SelectTriggerSheetService);
  trigger = input.required<TriggerShortModel | null>();

  protected readonly TriggerSelectionStep = TriggerSelectionStep;
}
