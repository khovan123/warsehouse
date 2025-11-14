import React from 'react';

export type RouteConfig = {
  path: string;
  component: React.ComponentType;
};

export type SwitchRouterProps = {
  loginedIn: boolean;
};

type LayoutTemplateProps = {
  children: React.ReactNode;
};

export type PrivateRouteConfig = {
  id: string;
  template: React.ComponentType<LayoutTemplateProps>;
  routes: RouteConfig[];
};

export type RouterType = {
  loginedIn: boolean;
};
