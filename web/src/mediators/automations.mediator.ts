import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { map, Observable } from 'rxjs';
import {
  ActionShortModel,
  AutomationModel,
  AutomationSchemaModel,
  AutomationSchemaService,
  AutomationSchemaTrigger,
  TriggerShortModel,
} from '@models/automation';
import {
  AutomationSchemaDTO,
  TriggerDTO,
  ActionDTO,
  ActionShortDTO,
} from '@repositories/automations/dto';

@Injectable({
  providedIn: 'root',
})
export class AutomationsMediator {
  readonly #automationsRepository = inject(AutomationsRepository);

  create(): Observable<string> {
    return this.#automationsRepository.post();
  }

  getById(id: string): Observable<AutomationModel> {
    return this.#automationsRepository.getById(id).pipe(
      map(dto => {
        return new AutomationModel(
          dto.id,
          dto.ownerId,
          dto.label,
          dto.description,
          dto.enabled,
          dto.updatedAt,
          '#EE883A',
          'chat-bubble-bottom-center-text',
          new TriggerShortModel(
            dto.trigger.identifier,
            dto.trigger.parameters,
            dto.trigger.dependencies
          ),
          this._mapActions(dto.actions)
        );
      })
    );
  }

  getSchema(): Observable<AutomationSchemaModel> {
    return this.#automationsRepository.getSchema().pipe(
      map((dto: AutomationSchemaDTO) => {
        const services: Record<string, AutomationSchemaService> = {};
        for (const serviceName in dto) {
          const service = dto[serviceName];
          const schemaTriggers = this._mapSchemaServiceTriggers(
            service.triggers
          );
          const schemaActions = this._mapSchemaServiceActions(service.actions);
          services[serviceName] = {
            name: service.name,
            color: service.color,
            iconUri: service.iconUri,
            triggers: schemaTriggers,
            actions: schemaActions,
          };
        }
        return new AutomationSchemaModel(services);
      })
    );
  }

  _mapSchemaServiceTriggers(
    dtoTriggers: Record<string, TriggerDTO>
  ): Record<string, AutomationSchemaTrigger> {
    const schemaTriggers: Record<string, AutomationSchemaTrigger> = {};

    for (const [identifier, trigger] of Object.entries(dtoTriggers)) {
      schemaTriggers[identifier] = {
        name: trigger.name,
        description: trigger.description,
        icon: trigger.icon,
        parameters: trigger.parameters,
        facts: trigger.facts,
      };
    }
    return schemaTriggers;
  }

  _mapSchemaServiceActions(
    dtoActions: Record<string, ActionDTO>
  ): Record<string, AutomationSchemaTrigger> {
    const schemaActions: Record<string, AutomationSchemaTrigger> = {};

    for (const [identifier, action] of Object.entries(dtoActions)) {
      schemaActions[identifier] = {
        name: action.name,
        description: action.description,
        icon: action.icon,
        parameters: action.parameters,
        facts: action.facts,
      };
    }
    return schemaActions;
  }

  _mapActions(actions: ActionShortDTO[]): ActionShortModel[] {
    return actions.map(
      action =>
        new ActionShortModel(
          action.identifier,
          action.parameters,
          action.dependencies
        )
    );
  }
}
