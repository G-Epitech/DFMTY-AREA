import { AutomationModel, newDefaultAutomationModel } from '@models/automation';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';
import { computed, inject } from '@angular/core';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, tap } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AutomationsMediator } from '@mediators/automations.mediator';
import { tapResponse } from '@ngrx/operators';

export interface AutomationWorkspaceState {
  automation: AutomationModel;
  error: boolean;
  loading: boolean;
}

const initialState: AutomationWorkspaceState = {
  automation: newDefaultAutomationModel(),
  error: false,
  loading: false,
};

export const AutomationWorkspaceStore = signalStore(
  withState(initialState),
  withComputed(store => ({
    getAutomation: computed(() => {
      return store.automation();
    }),
    automationHasChanged: computed(() => {
      return store.automation() !== initialState.automation;
    }),
    automationIsValid: computed(() => {
      return store.automation().hasTrigger;
    }),
  })),
  withMethods((store, automationsMediator = inject(AutomationsMediator)) => ({
    getById: rxMethod<string>(
      pipe(
        tap(() => patchState(store, { loading: true })),
        switchMap(id =>
          automationsMediator.getById(id).pipe(
            tapResponse({
              next: automation => {
                patchState(store, { automation, loading: false });
              },
              error: () => {
                patchState(store, { error: true, loading: false });
              },
            })
          )
        )
      )
    ),
  }))
);
