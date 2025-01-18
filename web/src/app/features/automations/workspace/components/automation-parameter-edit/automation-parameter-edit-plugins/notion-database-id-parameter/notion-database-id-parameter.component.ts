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
import { NotionDatabaseModel } from '@models/integration';
import { AsyncPipe, NgClass, NgOptimizedImage } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-notion-database-id-parameter',
  imports: [AsyncPipe, TrButtonDirective, NgClass, NgIcon, NgOptimizedImage],
  templateUrl: './notion-database-id-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class NotionDatabaseIdParameterComponent
  implements ParameterEditDynamicComponent, OnInit
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  readonly #notionMediator = inject(NotionMediator);

  databases$: Observable<NotionDatabaseModel[]> | undefined;

  ngOnInit() {
    if (this.integrationId) {
      this.databases$ = this.#notionMediator.getDatabases(this.integrationId);
    }
  }

  selectDatabase(database: NotionDatabaseModel) {
    this.valueChange.emit({ rawValue: database.id });
  }

  isDatabaseSelected(database: NotionDatabaseModel): boolean {
    return database.id === this.parameter.value;
  }
}
