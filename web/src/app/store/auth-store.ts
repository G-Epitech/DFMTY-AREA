import { UserModel } from '@models/user.model';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';
import { computed, inject } from '@angular/core';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { concatMap, pipe, tap } from 'rxjs';
import { tapResponse } from '@ngrx/operators';
import { TokenMediator } from '@mediators/token.mediator';
import { UsersMediator } from '@mediators/users.mediator';

export interface AuthState {
  user: UserModel | undefined | null;
  loading: boolean;
}

const initialState: AuthState = {
  user: undefined,
  loading: true,
};

export const AuthStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed((store, tokenMediator = inject(TokenMediator)) => ({
    getUser: computed(() => {
      return store.user();
    }),
    isAuthenticated: computed(() => {
      return (
        store.user() !== null &&
        store.user() !== undefined &&
        tokenMediator.accessTokenIsValid()
      );
    }),
    isLoading: computed(() => store.loading()),
  })),
  withMethods((store, usersMediator = inject(UsersMediator)) => ({
    me: rxMethod<void>(
      pipe(
        tap(() => patchState(store, { user: undefined, loading: true })),
        concatMap(() => {
          return usersMediator.me().pipe(
            tapResponse({
              next: user => patchState(store, { user, loading: false }),
              error: () => patchState(store, { user: null, loading: false }),
            })
          );
        })
      )
    ),
    reset: () => patchState(store, initialState),
    cancel: () => patchState(store, { loading: false }),
  }))
);
