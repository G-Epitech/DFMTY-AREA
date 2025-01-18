import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input, output,
  signal,
  Signal,
} from '@angular/core';
import { AutomationsWorkspaceStore } from '@features/automations/workspace/automations-workspace.store';
import {
  ActionModel,
  ActionParameter,
  AutomationSchemaFactModel,
  AutomationSchemaModel,
  TriggerModel,
  TriggerParameter,
} from '@models/automation';
import { SchemaStore } from '@app/store/schema-store';
import { DeepAutomationFact } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-previous-facts/automation-parameter-edit-previous-facts.types';
import { PreviousFactButtonComponent } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit-previous-facts/previous-fact-button/previous-fact-button.component';

@Component({
  selector: 'tr-automation-parameter-edit-previous-facts',
  imports: [PreviousFactButtonComponent],
  templateUrl: './automation-parameter-edit-previous-facts.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AutomationParameterEditPreviousFactsComponent {
  readonly #workspaceStore = inject(AutomationsWorkspaceStore);
  readonly #schemaStore = inject(SchemaStore);

  schema: AutomationSchemaModel | null = null;
  readonly parameter = input.required<ActionParameter | TriggerParameter>();
  trigger: Signal<TriggerModel | null> = this.#workspaceStore.getTrigger;
  actions: Signal<ActionModel[]> = this.#workspaceStore.getActions;

  facts = signal<DeepAutomationFact[]>([]);

  previousFactSelected = output<DeepAutomationFact>();

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
      identifier: actionIdx ? `${actionIdx}.${identifier}` : `T.${identifier}`,
      name: fact.name,
      description: fact.description,
      type: fact.type,
      idx: actionIdx,
      eventIdentifier: eventIdentifier,
    })) as DeepAutomationFact[];
  }

  onFactSelected(fact: DeepAutomationFact) {
    this.previousFactSelected.emit(fact);
  }
}
