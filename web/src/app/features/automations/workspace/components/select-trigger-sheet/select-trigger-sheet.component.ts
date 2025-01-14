import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { TrSheetImports } from '@triggo-ui/sheet';
import { TrInputDirective } from '@triggo-ui/input';
import { LabelDirective } from '@triggo-ui/label';
import { TrButtonDirective } from '@triggo-ui/button';
import { BrnSheetImports } from '@spartan-ng/ui-sheet-brain';
import { TriggerCardComponent } from '@features/automations/workspace/components/trigger-card/trigger-card.component';
import { TriggerShortModel } from '@models/automation';

@Component({
  standalone: true,
  selector: 'tr-select-trigger-sheet',
  imports: [
    TrSheetImports,
    TrInputDirective,
    LabelDirective,
    TrButtonDirective,
    BrnSheetImports,
    TriggerCardComponent,
  ],
  templateUrl: './select-trigger-sheet.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SelectTriggerSheetComponent {
  trigger = input.required<TriggerShortModel | null>();
}
