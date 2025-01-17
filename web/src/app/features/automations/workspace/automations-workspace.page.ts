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
import { ActionCardComponent } from '@features/automations/workspace/components/cards/action-card/action-card.component';
import { TrButtonDirective } from '@triggo-ui/button';
import { AddStepButtonComponent } from '@features/automations/workspace/components/add-step-button/add-step-button.component';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';
import { SelectTriggerSheetComponent } from '@features/automations/workspace/components/select-trigger-sheet/select-trigger-sheet.component';

@Component({
  selector: 'tr-automations-workspace',
  imports: [
    NgIcon,
    NgStyle,
    ActionCardComponent,
    TrButtonDirective,
    AddStepButtonComponent,
    SelectTriggerSheetComponent,
  ],
  templateUrl: './automations-workspace.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  providers: [AutomationWorkspaceStore],
})
export class AutomationsWorkspacePageComponent implements OnInit, OnDestroy {
  readonly #route = inject(ActivatedRoute);
  readonly #workspaceStore = inject(AutomationWorkspaceStore);

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
