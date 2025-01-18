import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { map, Observable } from 'rxjs';
import {
  AutomationModel,
  AutomationSchemaModel,
  AutomationSchemaService,
  TriggerModel,
} from '@models/automation';
import { AutomationSchemaDTO } from '@repositories/automations/dto';
import { AutomationMapperService } from '@mediators/mappers';

@Injectable({
  providedIn: 'root',
})
export class AutomationsMediator {
  readonly #automationsRepository = inject(AutomationsRepository);
  readonly #automationMapper = inject(AutomationMapperService);

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
          new TriggerModel(
            dto.trigger.identifier,
            dto.trigger.parameters,
            dto.trigger.dependencies
          ),
          this.#automationMapper.mapActions(dto.actions)
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
          const schemaTriggers =
            this.#automationMapper.mapSchemaServiceTriggers(service.triggers);
          const schemaActions = this.#automationMapper.mapSchemaServiceActions(
            service.actions
          );
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
}
