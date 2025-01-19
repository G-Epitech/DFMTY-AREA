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
import { ToastrService } from 'ngx-toastr';
import { AppRouter } from '@app/app.router';

export interface AutomationWorkspaceState {
  automation: AutomationModel;
  error: boolean;
  loading: boolean;
  initialState: AutomationModel;
}

const initialState: AutomationWorkspaceState = {
  automation: newDefaultAutomationModel(),
  error: false,
  loading: false,
  initialState: newDefaultAutomationModel(),
};

export const AutomationsWorkspaceStore = signalStore(
  withState(initialState),
  withComputed(store => ({
    getAutomation: computed(() => {
      return store.automation();
    }),
    isLoading: computed(() => {
      return store.loading();
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
    getLabel: computed(() => {
      return store.automation().label;
    }),
    getDescription: computed(() => {
      return store.automation().description;
    }),
    canSave: computed(() => {
      const trigger = store.automation().trigger;
      if (!trigger) {
        return false;
      }
      for (const param of trigger.parameters) {
        if (!param.value) {
          return false;
        }
      }
      for (const action of store.automation().actions) {
        for (const param of action.parameters) {
          if (!param.value) {
            return false;
          }
        }
      }
      return store.automation() !== store.initialState();
    }),
  })),
  withMethods(
    (
      store,
      automationsMediator = inject(AutomationsMediator),
      toastr = inject(ToastrService),
      appRouter = inject(AppRouter)
    ) => ({
      updateLabel: rxMethod<string>(
        pipe(
          tap(label => {
            const currentAutomation = store.automation();
            const updatedAutomation = new AutomationModel(
              currentAutomation.id,
              currentAutomation.ownerId,
              label,
              currentAutomation.description,
              currentAutomation.enabled,
              currentAutomation.updatedAt,
              currentAutomation.color,
              currentAutomation.icon,
              currentAutomation.trigger,
              currentAutomation.actions
            );
            patchState(store, { automation: updatedAutomation });
          })
        )
      ),
      updateDescription: rxMethod<string>(
        pipe(
          tap(description => {
            const currentAutomation = store.automation();
            const updatedAutomation = new AutomationModel(
              currentAutomation.id,
              currentAutomation.ownerId,
              currentAutomation.label,
              description,
              currentAutomation.enabled,
              currentAutomation.updatedAt,
              currentAutomation.color,
              currentAutomation.icon,
              currentAutomation.trigger,
              currentAutomation.actions
            );
            patchState(store, { automation: updatedAutomation });
          })
        )
      ),
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
      create: rxMethod<void>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(() =>
            automationsMediator.create(store.automation()).pipe(
              tapResponse({
                next: () => {
                  toastr.success('Automation created successfully', 'Success');
                  patchState(store, { loading: false });
                  appRouter.redirectToAutomationListing();
                },
                error: () => {
                  patchState(store, { error: true, loading: false });
                  toastr.error(
                    'Failed to create automation. Check your parameters or try again later',
                    'Error'
                  );
                },
              })
            )
          )
        )
      ),
      getById: rxMethod<string>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(id =>
            automationsMediator.getById(id).pipe(
              tapResponse({
                next: automation => {
                  patchState(store, {
                    automation,
                    loading: false,
                    initialState: automation,
                  });
                },
                error: () => {
                  patchState(store, { error: true, loading: false });
                },
              })
            )
          )
        )
      ),
    })
  )
);
