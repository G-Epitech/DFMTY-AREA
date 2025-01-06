import {
  ChangeDetectionStrategy,
  Component,
  inject,
} from '@angular/core';
import { AutomationModel } from '@models/automation';
import { iconName } from '@utils/icon';
import { NgIcon } from '@ng-icons/core';
import { AsyncPipe, NgStyle } from '@angular/common';
import { ContextMenuComponent } from '@features/automations/workspace/components/context-menu/context-menu.component';
import {
  map,
  Observable,
  Subject,
  switchMap,
  takeUntil,
} from 'rxjs';
import { ActionCardComponent } from '@features/automations/workspace/components/action-card/action-card.component';
import { AutomationsMediator } from '@mediators/automations.mediator';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'tr-automations-workspace',
  imports: [
    NgIcon,
    NgStyle,
    ContextMenuComponent,
    AsyncPipe,
    ActionCardComponent,
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

  readonly steps = ['Message received in channel', 'Send message to channel'];

  automation$: Observable<AutomationModel> = this.#route.params.pipe(
    takeUntil(this.destory$),
    map(params => params['id'] as string),
    switchMap(id => this.#automationMediator.getById(id))
  );

  protected readonly iconName = iconName;
}
