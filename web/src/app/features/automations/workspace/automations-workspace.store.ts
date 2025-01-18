import {
  ActionModel,
  AutomationModel,
  newDefaultAutomationModel,
  TriggerModel,
} from '@models/automation';
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

export const AutomationsWorkspaceStore = signalStore(
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
    getTrigger: computed(() => {
      return store.automation().trigger;
    }),
    getActions: computed(() => {
      return store.automation().actions;
    }),
  })),
  withMethods((store, automationsMediator = inject(AutomationsMediator)) => ({
    addTrigger: rxMethod<TriggerModel>(
      pipe(
        tap(trigger => {
          const currentAutomation = store.automation();
          const updatedAutomation = new AutomationModel(
            currentAutomation.id,
            currentAutomation.ownerId,
            currentAutomation.label,
            currentAutomation.description,
            currentAutomation.enabled,
            currentAutomation.updatedAt,
            currentAutomation.color,
            currentAutomation.icon,
            trigger,
            currentAutomation.actions
          );
          patchState(store, { automation: updatedAutomation });
        })
      )
    ),
    updateActions: rxMethod<{ idx: number; action: ActionModel }>(
      pipe(
        tap(({ idx, action }) => {
          const currentAutomation = store.automation();
          if (currentAutomation.actions.length < idx) {
            return;
          }

          const updatedActions = [...currentAutomation.actions];
          updatedActions[idx] = action;
          const updatedAutomation = new AutomationModel(
            currentAutomation.id,
            currentAutomation.ownerId,
            currentAutomation.label,
            currentAutomation.description,
            currentAutomation.enabled,
            currentAutomation.updatedAt,
            currentAutomation.color,
            currentAutomation.icon,
            currentAutomation.trigger,
            updatedActions
          );
          patchState(store, { automation: updatedAutomation });
        })
      )
    ),

    getAction: (idx: number) =>
      computed(() => {
        if (store.automation().actions.length < idx) {
          return null;
        }
        return store.automation().actions[idx];
      }),
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
