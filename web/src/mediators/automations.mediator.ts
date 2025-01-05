import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { map, Observable } from 'rxjs';
import { AutomationModel } from '@models/automation';

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
          dto.trigger,
          dto.actions
        );
      })
    );
  }
}
