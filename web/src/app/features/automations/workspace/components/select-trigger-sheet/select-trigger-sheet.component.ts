import {
  ChangeDetectionStrategy,
  Component,
  input,
  signal,
} from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/trigger-card/trigger-card.component';
import { TriggerShortModel } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TriggerSelectionStep } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.types';
import { IntegrationSelectionComponent } from '@features/automations/workspace/components/select-trigger-sheet/integration-selection/integration-selection.component';
import { AvailableIntegrationType } from '@common/types';
import {
  AvailableIntegrationListCardComponent
} from '@components/available-integration-list-card/available-integration-list-card.component';

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
  selectionStep = signal<TriggerSelectionStep>(TriggerSelectionStep.MAIN);
  stepHistory = signal<TriggerSelectionStep[]>([TriggerSelectionStep.MAIN]);
  selectedIntegration = signal<AvailableIntegrationType | null>(null);

  goToIntegrationSelection() {
    this.selectionStep.set(TriggerSelectionStep.INTEGRATION);
    this.stepHistory.set([
      ...this.stepHistory(),
      TriggerSelectionStep.INTEGRATION,
    ]);
  }

  selectIntegration(integration: AvailableIntegrationType): void {
    this.selectedIntegration.set(integration);
    this.back();
  }

  back() {
    const history = this.stepHistory();
    if (history.length <= 1) {
      return;
    }
    history.pop();
    this.stepHistory.set(history);
    this.selectionStep.set(history[history.length - 1]);
  }

  protected readonly TriggerSelectionStep = TriggerSelectionStep;
}
