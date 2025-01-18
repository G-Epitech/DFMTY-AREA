import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  signal,
  Signal,
} from '@angular/core';
import { AutomationsWorkspaceStore } from '@features/automations/workspace/automations-workspace.store';
import {
  ActionModel,
  AutomationParameterValueType,
  AutomationSchemaFactModel,
  AutomationSchemaModel,
  TriggerModel,
} from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { PascalToPhrasePipe } from '@app/pipes';

interface DeepAutomationFact {
  eventIdentifier: string;
  identifier: string;
  name: string;
  description: string;
  type: AutomationParameterValueType;
  idx: number | null;
}

@Component({
  selector: 'tr-automation-parameter-edit-previous-facts',
  imports: [PascalToPhrasePipe],
  templateUrl: './automation-parameter-edit-previous-facts.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditPreviousFactsComponent {
  readonly #workspaceStore = inject(AutomationsWorkspaceStore);
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;

  trigger: Signal<TriggerModel | null> = this.#workspaceStore.getTrigger;
  actions: Signal<ActionModel[]> = this.#workspaceStore.getActions;

  facts = signal<DeepAutomationFact[]>([]);

  constructor() {
    effect(() => {
      const schema = this.#schemaStore.getSchema();
      if (this.trigger() && this.actions() && schema) {
        this.schema = schema;
        this._fillFacts();
      }
    });
  }

  _fillFacts() {
    let facts: DeepAutomationFact[] = [];

    const triggerFacts = this.schema?.getTriggerFacts(this.trigger()!);
    if (triggerFacts) {
      facts = facts.concat(
        this._mapSchemaFactsToDeepFacts(
          triggerFacts,
          this.trigger()!.nameIdentifier
        )
      );
    }
    for (const [index, action] of this.actions().entries()) {
      const actionFacts = this.schema?.getActionFacts(action);
      if (actionFacts) {
        facts = facts.concat(
          this._mapSchemaFactsToDeepFacts(
            actionFacts,
            action.nameIdentifier,
            index + 1
          )
        );
      }
    }
    this.facts.set(facts);
  }

  _mapSchemaFactsToDeepFacts(
    facts: Record<string, AutomationSchemaFactModel>,
    eventIdentifier: string,
    actionIdx: number | null = null
  ): DeepAutomationFact[] {
    return Object.entries(facts).map(([identifier, fact]) => ({
      identifier,
      name: fact.name,
      description: fact.description,
      type: fact.type,
      idx: actionIdx,
      eventIdentifier: eventIdentifier,
    })) as DeepAutomationFact[];
  }
}
