import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { Observable } from 'rxjs';
import { AutomationDTO } from '@repositories/dto';

@Injectable({
  providedIn: 'root',
})
export class AutomationsMediator {
  readonly #automationsRepository = inject(AutomationsRepository);

  create(): Observable<string> {
    return this.#automationsRepository.post();
  }

  getById(id: string): Observable<AutomationDTO> {
    return this.#automationsRepository.getById(id);
  }
}
