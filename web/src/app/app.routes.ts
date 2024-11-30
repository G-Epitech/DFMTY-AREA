import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () =>
      import('@features/home/home.page').then(m => m.HomePage),
    pathMatch: 'full',
  },
  {
    path: 'login',
    loadComponent: () =>
      import('@features/authentication/login/login.page').then(
        m => m.LoginPage
      ),
    pathMatch: 'full',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('@features/authentication/register/register.page').then(
        m => m.RegisterPage
      ),
    pathMatch: 'full',
  },
];
