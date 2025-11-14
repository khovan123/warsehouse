import React from 'react';

import PrivateLayout from '../components/templates/PrivateLayout/PrivateLayout';

import { LANDING_PATH, LOGIN_PATH, TOP_PATH } from './route.constants';
import type { PrivateRouteConfig, RouteConfig } from './type';

export const LoginPage = React.lazy(() =>
  import('../components/pages/LoginPage').then(({ LoginPage }) => ({
    default: LoginPage,
  }))
);
export const LandingPage = React.lazy(() =>
  import('../components/pages/LandingPage').then(({ LandingPage }) => ({
    default: LandingPage,
  }))
);
export const TopPage = React.lazy(() =>
  import('../components/pages/TopPage').then(({ TopPage }) => ({
    default: TopPage,
  }))
);

export const PUBLIC_ROUTES: RouteConfig[] = [
  {
    path: LANDING_PATH,
    component: LandingPage,
  },
  {
    path: LOGIN_PATH,
    component: LoginPage,
  },
];

export const PRIVATE_ROUTES: PrivateRouteConfig[] = [
  {
    id: 'private-layout',
    template: PrivateLayout,
    routes: [
      {
        path: TOP_PATH,
        component: TopPage,
      },
    ],
  },
];
