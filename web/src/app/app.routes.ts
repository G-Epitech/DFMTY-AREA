import {Routes} from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadComponent: () => import('@features/home/home.page').then(m => m.HomePage)
  },
  {
    path: 'register',
    loadComponent: () => import('@features/authentication/register/register.page').then(m => m.RegisterPage)
  }
];
