import { inject, Injectable } from '@angular/core';
import { AutomationsRepository } from '@repositories/automations';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AutomationsMediator {
  readonly #automationsRepository = inject(AutomationsRepository);

  create(): Observable<string> {
    return this.#automationsRepository.post();
  }
}
