import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { AuthStore } from '@app/store';
import { AppRouter } from '@app/app.router';
import { switchMap, filter, map } from 'rxjs/operators';
import { AuthMediator } from '@mediators/auth.mediator';

export const authGuard: CanActivateFn = () => {
  const store = inject(AuthStore);
  const authMediator = inject(AuthMediator);
  const appRouter = inject(AppRouter);

  const isAuthenticated$ = toObservable(store.isAuthenticated);
  const isLoading$ = toObservable(store.isLoading);

  return isLoading$.pipe(
    filter(isLoading => !isLoading),
    switchMap(() => {
      return isAuthenticated$.pipe(
        map(isAuthenticated => {
          if (!isAuthenticated) {
            authMediator.logout();
            appRouter.redirectToLanding();
            return false;
          }
          return true;
        })
      );
    })
  );
};
