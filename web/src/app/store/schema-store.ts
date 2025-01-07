import { AutomationSchemaModel } from '@models/automation';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';
import { computed, inject } from '@angular/core';
import { AutomationsMediator } from '@mediators/automations.mediator';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { concatMap, pipe, take, tap } from 'rxjs';
import { tapResponse } from '@ngrx/operators';
import { TokenMediator } from '@mediators/token.mediator';

export interface SchemaState {
  automationSchema: AutomationSchemaModel | undefined | null;
  isLoading: boolean;
}

const initialState: SchemaState = {
  automationSchema: undefined,
  isLoading: false,
};

export const SchemaStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed((store) => ({
    getSchema: computed(() => {
      return store.automationSchema();
    }),
  })),
  withMethods((store, automationsMediator = inject(AutomationsMediator)) => ({
    initialize: rxMethod<void>(
      pipe(
        take(1),
        tap(() =>
          patchState(store, { automationSchema: undefined, isLoading: true })
        ),
        concatMap(() => {
          return automationsMediator.getSchema().pipe(
            tapResponse({
              next: schema =>
                patchState(store, {
                  automationSchema: schema,
                  isLoading: false,
                }),
              error: () =>
                patchState(store, { automationSchema: null, isLoading: false }),
            })
          );
        })
      )
    ),
  }))
);
