import {
  ChangeDetectionStrategy,
  Component,
  inject,
  Signal,
} from '@angular/core';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrDialogImports } from '@triggo-ui/dialog';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';
import { AutomationsWorkspaceStore } from '@features/automations/workspace/automations-workspace.store';

@Component({
  selector: 'tr-general-edit-dialog',
  imports: [TrDialogImports, BrnDialogImports, TrButtonDirective, NgIcon],
  templateUrl: './general-edit-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GeneralEditDialogComponent {
  readonly #workspaceStore = inject(AutomationsWorkspaceStore);

  label: Signal<string> = this.#workspaceStore.getLabel;
  description: Signal<string> = this.#workspaceStore.getDescription;
}
