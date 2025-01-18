import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  OnInit,
} from '@angular/core';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterValueType } from '@models/automation';
import { NotionMediator } from '@mediators/integrations';
import { Observable } from 'rxjs';
import { NotionPageModel } from '@models/integration';
import { AsyncPipe, NgClass } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';

@Component({
  selector: 'tr-notion-parent-id-parameter',
  imports: [AsyncPipe, NgIcon, TrButtonDirective, NgClass],
  templateUrl: './notion-parent-id-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class NotionParentIdParameterComponent
  implements ParameterEditDynamicComponent, OnInit
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  readonly #notionMediator = inject(NotionMediator);

  pages$: Observable<NotionPageModel[]> | undefined;

  ngOnInit() {
    if (this.integrationId) {
      this.pages$ = this.#notionMediator.getPages(this.integrationId);
    }
  }

  selectPage(page: NotionPageModel) {
    this.valueChange.emit({ rawValue: page.id });
  }

  isPageSelected(page: NotionPageModel): boolean {
    return page.id === this.parameter.value;
  }
}
