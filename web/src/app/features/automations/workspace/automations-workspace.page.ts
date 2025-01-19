import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { NgIcon } from '@ng-icons/core';
import { NgStyle } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { TrButtonDirective } from '@triggo-ui/button';
import { AutomationsWorkspaceStore } from '@features/automations/workspace/automations-workspace.store';
import { SelectTriggerSheetComponent } from '@features/automations/workspace/components/edit-sheets/select-trigger-sheet/select-trigger-sheet.component';
import { SelectActionSheetComponent } from '@features/automations/workspace/components/edit-sheets/select-action-sheet/select-action-sheet.component';

@Component({
  selector: 'tr-automations-workspace',
  imports: [
    NgIcon,
    NgStyle,
    TrButtonDirective,
    SelectTriggerSheetComponent,
    SelectActionSheetComponent,
  ],
  templateUrl: './automations-workspace.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  providers: [AutomationsWorkspaceStore],
})
export class AutomationsWorkspacePageComponent implements OnInit, OnDestroy {
  readonly #route = inject(ActivatedRoute);
  readonly #workspaceStore = inject(AutomationsWorkspaceStore);

  private destroy$ = new Subject<void>();

  automation = this.#workspaceStore.getAutomation;

  ngOnInit() {
    this.#route.params.pipe(takeUntil(this.destroy$)).subscribe(params => {
      if (params['id']) {
        this.#workspaceStore.getById(params['id']);
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
