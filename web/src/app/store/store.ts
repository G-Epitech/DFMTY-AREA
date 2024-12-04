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
  isLoading: boolean;
}

const initialState: AuthState = {
  user: undefined,
  isLoading: false,
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
  })),
  withMethods((store, usersMediator = inject(UsersMediator)) => ({
    me: rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        concatMap(() => {
          return usersMediator.me().pipe(
            tapResponse({
              next: user => patchState(store, { user, isLoading: false }),
              error: error => {
                console.error('Failed to get user', error);
                patchState(store, { user: null, isLoading: false });
              },
            })
          );
        })
      )
    ),
    reset: () => patchState(store, initialState),
  }))
);
