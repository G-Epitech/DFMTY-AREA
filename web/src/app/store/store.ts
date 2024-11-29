import {AuthUserModel} from '@models/auth-user.model';
import {patchState, signalStore, withMethods, withState} from '@ngrx/signals';
import {inject} from '@angular/core';
import {AuthMediator} from '@mediators/auth.mediator';
import {rxMethod} from '@ngrx/signals/rxjs-interop';
import {concatMap, pipe, tap} from 'rxjs';
import {tapResponse} from '@ngrx/operators';

export interface AuthState {
  user: AuthUserModel | undefined | null;
  isLoading: boolean;
}

const initialState: AuthState = {
  user: undefined,
  isLoading: false
}

export const AuthStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods((store, authMediator = inject(AuthMediator)) => ({
    me: rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        concatMap(() => {
          return authMediator.me().pipe(
            tapResponse({
              next: (user) => patchState(store, { user, isLoading: false }),
              error: (error) => patchState(store, { user: null, isLoading: false })
            })
          )
        })
      )
    ),
    isAuthenticated: () => store.user instanceof AuthUserModel,
  }))
);
