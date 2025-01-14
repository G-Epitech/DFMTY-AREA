import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AutomationModel } from '@models/automation';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';
import { AsyncPipe, NgStyle } from '@angular/common';
import { map, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { AutomationsMediator } from '@mediators/automations.mediator';
import { ActivatedRoute } from '@angular/router';
import { TriggerCardComponent } from '@features/automations/workspace/components/trigger-card/trigger-card.component';
import { ActionCardComponent } from '@features/automations/workspace/components/action-card/action-card.component';
import { TrButtonDirective } from '@triggo-ui/button';
import { AddStepButtonComponent } from '@features/automations/workspace/components/add-step-button/add-step-button.component';

@Component({
  selector: 'tr-automations-workspace',
  imports: [
    NgIcon,
    NgStyle,
    AsyncPipe,
    TriggerCardComponent,
    ActionCardComponent,
    TrButtonDirective,
    AddStepButtonComponent,
  ],
  templateUrl: './automations-workspace.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationsWorkspacePageComponent {
  readonly #automationMediator = inject(AutomationsMediator);
  readonly #route = inject(ActivatedRoute);

  private destory$ = new Subject<void>();

  automation$: Observable<AutomationModel> = this.#route.params.pipe(
    takeUntil(this.destory$),
    map(params => params['id'] as string),
    switchMap(id => this.#automationMediator.getById(id))
  );

  protected readonly iconName = iconName;
}
