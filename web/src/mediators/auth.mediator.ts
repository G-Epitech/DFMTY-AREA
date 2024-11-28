import {inject, Injectable} from '@angular/core';
import {AuthRepository} from '@repositories/auth';
import {Observable, tap} from 'rxjs';
import {TokensModel} from '@models/tokens.model';

@Injectable({
  providedIn: 'root'
})
export class AuthMediator {
  readonly #authRepository = inject(AuthRepository);

  register(email: string, password: string, firstName: string, lastName: string): Observable<TokensModel> {
    return this.#authRepository.register({
      email: email,
      password: password,
      firstName: firstName,
      lastName: lastName
    }).pipe(
      tap(tokens => this.#authRepository.storeTokens(tokens))
    );
  }
}
