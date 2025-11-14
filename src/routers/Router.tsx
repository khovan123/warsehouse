import React, { Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router';

import { LandingPage } from '../components/pages/LandingPage';
import MainLayout from '../components/templates/MainLayout/MainLayout';
import PrivateLayout from '../components/templates/PrivateLayout/PrivateLayout';

import { LOGIN_PATH } from './route.constants';
import { PRIVATE_ROUTES, PUBLIC_ROUTES, TopPage } from './route.definitions';
import type { RouterType, SwitchRouterProps } from './type';

const NotFoundPage = React.lazy(() =>
  import('../components/pages/NotFoundPage').then(({ NotFoundPage }) => ({
    default: NotFoundPage,
  }))
);

const ErrorPage = React.lazy(() =>
  import('../components/pages/ErrorPage').then(({ ErrorPage }) => ({
    default: ErrorPage,
  }))
);

const Spinner = (): React.ReactElement => {
  return <p>Spinder</p>;
};

const SwitchRouter: React.FC<SwitchRouterProps> = ({ loginedIn }) => {
  return (
    <Routes>
      <Route path="/">
        {PUBLIC_ROUTES.map(({ path, component: Component }) => (
          <Route
            key={path}
            path={path}
            element={
              <Suspense fallback={<Spinner />}>
                <Component />
              </Suspense>
            }
          />
        ))}

        {loginedIn ? (
          <>
            <Route
              path=""
              element={
                <Suspense fallback={<Spinner />}>
                  <MainLayout />
                </Suspense>
              }
            >
              {PRIVATE_ROUTES.map(({ id, routes, template: LayoutTemplate }) => {
                return routes.map(({ component: Component, path }) => (
                  <Route
                    key={`${id}-${path}`}
                    path={path}
                    element={
                      <Suspense fallback={<Spinner />}>
                        <LayoutTemplate>
                          <Component />
                        </LayoutTemplate>
                      </Suspense>
                    }
                  />
                ));
              })}
              <Route
                path="/"
                element={
                  <Suspense fallback={<Spinner />}>
                    <PrivateLayout>
                      <TopPage />
                    </PrivateLayout>
                  </Suspense>
                }
              />
            </Route>

            <Route
              path="/error"
              element={
                <Suspense fallback={<Spinner />}>
                  <ErrorPage />
                </Suspense>
              }
            />

            <Route
              path="*"
              element={
                <Suspense>
                  <NotFoundPage />
                </Suspense>
              }
            />
          </>
        ) : (
          <>
            <Route
              path="/"
              element={
                <Suspense fallback={<Spinner />}>
                  <LandingPage />
                </Suspense>
              }
            />
            <Route path="*" element={<Navigate to={LOGIN_PATH} replace />} />
          </>
        )}
      </Route>
    </Routes>
  );
};

const Router = ({ loginedIn }: RouterType) => {
  return <SwitchRouter loginedIn={loginedIn} />;
};
export default Router;
