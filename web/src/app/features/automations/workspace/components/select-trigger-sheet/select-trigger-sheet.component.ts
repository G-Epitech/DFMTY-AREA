import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/trigger-card/trigger-card.component';
import { TriggerShortModel } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/select-trigger-sheet/integration-selection/integration-selection.component';
import { AvailableIntegrationType } from '@common/types';
import { AvailableIntegrationListCardComponent } from '@components/available-integration-list-card/available-integration-list-card.component';
import { patchState, signalState } from '@ngrx/signals';
import {
  SelectTriggerSheetState,
  stateUpdaterBack,
  stateUpdaterGoToIntegrationSelection,
  stateUpdaterSelectIntegration,
} from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.state';

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
  ],
  templateUrl: './select-trigger-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SelectTriggerSheetComponent {
  trigger = input.required<TriggerShortModel | null>();
  stateGroup = signalState<SelectTriggerSheetState>({
    selectionStep: TriggerSelectionStep.MAIN,
    stepHistory: [TriggerSelectionStep.MAIN],
    selectedIntegration: null,
  });

  goToIntegrationSelection() {
    patchState(this.stateGroup, stateUpdaterGoToIntegrationSelection());
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    patchState(this.stateGroup, stateUpdaterSelectIntegration(integration));
    this.back();
  }

  back() {
    patchState(this.stateGroup, stateUpdaterBack());
  }

  protected readonly TriggerSelectionStep = TriggerSelectionStep;
}
