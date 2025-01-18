import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { map, Observable } from 'rxjs';
import {
  AutomationModel,
  AutomationSchemaModel,
  AutomationSchemaService,
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
      map(dto => this.#automationMapper.mapAutomationModel(dto))
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
