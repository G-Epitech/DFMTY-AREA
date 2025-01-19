import { PublicLayoutComponent } from '@features/public/public-layout/public-layout.component';
import { Routes } from '@angular/router';
import { authGuard, stateGuard } from '@app/guards';
import { MainLayoutComponent } from '@features/layout/main.layout.component';
import { GOOGLE_STATE_CODE_KEY } from '@common/constants';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home',
  },
  {
    path: '',
    component: PublicLayoutComponent,
    children: [
      {
        path: 'landing',
        pathMatch: 'full',
        loadComponent: () =>
          import('@features/public/landing/landing.page').then(
            m => m.LandingPageComponent
          ),
      },
      {
        path: 'downloads',
        pathMatch: 'full',
        loadComponent: () =>
          import('@features/public/downloads/downloads.page').then(
            m => m.DownloadsPageComponent
          ),
      },
      {
        path: 'client.apk',
        pathMatch: 'full',
        loadComponent: () =>
          import('@features/public/downloads/download.client.page').then(
            m => m.DownloadClientComponent
          ),
      },
      {
        path: 'faq',
        pathMatch: 'full',
        loadComponent: () =>
          import('@features/public/faq/faq.page').then(m => m.FaqPageComponent),
      },
      {
        path: 'about-us',
        pathMatch: 'full',
        loadComponent: () =>
          import('@features/public/about-us/about-us.page').then(
            m => m.AboutUsPageComponent
          ),
      },
    ],
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'home',
        loadComponent: () =>
          import('@features/home/home.page').then(m => m.HomePageComponent),
        pathMatch: 'full',
      },
      {
        path: 'automations',
        loadComponent: () =>
          import('@features/automations/listing/automations-list.page').then(
            m => m.AutomationsListPageComponent
          ),
        pathMatch: 'full',
      },
      {
        path: 'automations/workspace',
        loadComponent: () =>
          import(
            '@features/automations/workspace/automations-workspace.page'
          ).then(m => m.AutomationsWorkspacePageComponent),
        pathMatch: 'full',
      },
      {
        path: 'automations/workspace/:id',
        loadComponent: () =>
          import(
            '@features/automations/workspace/automations-workspace.page'
          ).then(m => m.AutomationsWorkspacePageComponent),
      },
      {
        path: 'integrations',
        loadComponent: () =>
          import('@features/integrations/integrations.page').then(
            m => m.IntegrationsPageComponent
          ),
        pathMatch: 'full',
      },
      {
        path: 'settings',
        loadComponent: () =>
          import('@features/settings/settings.page').then(
            m => m.SettingsPageComponent
          ),
        pathMatch: 'full',
      },
    ],
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
  {
    path: 'oauth2',
    children: [
      {
        path: 'discord',
        loadComponent: () =>
          import('@features/oauth2/discord/discord.oauth2.page').then(
            m => m.DiscordOAuth2PageComponent
          ),
        pathMatch: 'full',
      },
      {
        path: 'gmail',
        loadComponent: () =>
          import('@features/oauth2/gmail/gmail.oauth2.page').then(
            m => m.GmailOAuth2PageComponent
          ),
        pathMatch: 'full',
      },
      {
        path: 'notion',
        loadComponent: () =>
          import('@features/oauth2/notion/notion.oauth2.page').then(
            m => m.NotionOauth2PageComponent
          ),
        pathMatch: 'full',
      },
      {
        path: 'google',
        loadComponent: () =>
          import('@features/oauth2/google/google.oauth2.page').then(
            m => m.GoogleOauth2PageComponent
          ),
        pathMatch: 'full',
        canActivate: [stateGuard],
        data: { stateKey: GOOGLE_STATE_CODE_KEY, redirectUrl: '/login' },
      },
    ],
  },
];
