import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AppRouter } from '@app/app.router';

export const stateGuard: CanActivateFn = route => {
  const router = inject(AppRouter);
  const stateKey = route.data['stateKey'] as string;
  const redirectUrl = route.data['redirectUrl'] as string;
  const stateCode = localStorage.getItem(stateKey);

  if (!stateCode) {
    void router.navigate([redirectUrl]);
    return false;
  }
  localStorage.removeItem(stateKey);
  const stateCodeParam = route.queryParams['state'];
  console.log(stateCodeParam);
  if (!stateCodeParam) {
    void router.navigate([redirectUrl]);
    return false;
  }
  if (stateCode !== stateCodeParam) {
    void router.navigate([redirectUrl]);
    return false;
  }
  return true;
};
