import React from 'react';

export type RouteConfig = {
  path: string;
  component: React.ComponentType;
};

export type SwitchRouterProps = {
  loginedIn: boolean;
};

export type PrivateRouteConfig = {
  id: string;
  template: React.ComponentType;
  routes: RouteConfig[];
};

export type RouterType = {
  loginedIn: boolean;
};
