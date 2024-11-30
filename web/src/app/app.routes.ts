import { Routes } from '@angular/router';
import { authGuard } from '@app/guards';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () =>
      import('@features/home/home.page').then(m => m.HomePageComponent),
    pathMatch: 'full',
    canActivate: [authGuard],
  },
  {
    path: 'login',
    loadComponent: () =>
      import('@features/authentication/login/login.page').then(
        m => m.LoginPageComponent
      ),
    pathMatch: 'full',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('@features/authentication/register/register.page').then(
        m => m.RegisterPageComponent
      ),
    pathMatch: 'full',
  },
];
