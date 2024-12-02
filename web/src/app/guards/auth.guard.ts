import { CanActivateFn } from '@angular/router';
import { AuthStore } from '@app/store';
import { inject } from '@angular/core';
import { AppRouter } from '@app/app.router';

export const authGuard: CanActivateFn = () => {
  const store = inject(AuthStore);
  const appRouter = inject(AppRouter);

  if (!store.isAuthenticated()) {
    appRouter.redirectToLogin();
    return false;
  }
  return true;
};
