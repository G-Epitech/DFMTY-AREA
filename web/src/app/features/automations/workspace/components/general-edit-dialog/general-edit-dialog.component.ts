import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
} from '@angular/core';
import { BrnDialogImports } from '@spartan-ng/ui-dialog-brain';
import { TrDialogImports } from '@triggo-ui/dialog';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';
import { AutomationsWorkspaceStore } from '@features/automations/workspace/automations-workspace.store';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TrInputDirective } from '@triggo-ui/input';

@Component({
  selector: 'tr-general-edit-dialog',
  imports: [
    TrDialogImports,
    BrnDialogImports,
    TrButtonDirective,
    NgIcon,
    ReactiveFormsModule,
    TrInputDirective,
  ],
  templateUrl: './general-edit-dialog.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class GeneralEditDialogComponent {
  readonly #workspaceStore = inject(AutomationsWorkspaceStore);

  form = new FormGroup({
    label: new FormControl(''),
    description: new FormControl(''),
  });

  isEditingLabel = false;
  isEditingDescription = false;

  constructor() {
    effect(() => {
      this.form.patchValue({
        label: this.#workspaceStore.getLabel(),
        description: this.#workspaceStore.getDescription(),
      });
    });
  }

  startEditingLabel() {
    this.isEditingLabel = true;
    setTimeout(() => {
      const input = document.querySelector('#labelInput');
      if (input) {
        (input as HTMLInputElement).focus();
      }
    });
  }

  stopEditingLabel() {
    this.isEditingLabel = false;
  }

  startEditingDescription() {
    this.isEditingDescription = true;
    setTimeout(() => {
      const input = document.querySelector('#descriptionInput');
      if (input) {
        (input as HTMLTextAreaElement).focus();
      }
    });
  }

  stopEditingDescription() {
    this.isEditingDescription = false;
  }

  onSubmit() {
    if (this.form.valid) {
      console.log('Form submitted');
    }
  }
}
