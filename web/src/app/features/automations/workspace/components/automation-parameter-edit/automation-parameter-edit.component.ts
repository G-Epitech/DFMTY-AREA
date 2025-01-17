import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { PascalToPhrasePipe } from '@app/pipes';
import { AutomationParameterType } from '@models/automation';
import { NgIcon } from '@ng-icons/core';
import { TrTabsImports } from '@triggo-ui/tabs';

@Component({
  standalone: true,
  selector: 'tr-automation-parameter-edit',
  imports: [PascalToPhrasePipe, NgIcon, TrTabsImports],
  templateUrl: './automation-parameter-edit.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AutomationParameterEditComponent {
  parameter = input.required<{ identifier: string; value: string | null }>();
  paramterType = input.required<AutomationParameterType>();
}
