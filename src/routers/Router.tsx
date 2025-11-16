import React, { Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router';

import Spinner from '../components/atoms/Spinder/Spinder';
import BlankLayOut from '../components/templates/BlankLayout/BlankLayOut';
import MainLayout from '../components/templates/MainLayout/MainLayout';
import PrivateLayout from '../components/templates/PrivateLayout/PrivateLayout';
import PublicLayOut from '../components/templates/PublicLayout/PublicLayOut';

import { LOGIN_PATH } from './route.constants';
import { LandingPage, PRIVATE_ROUTES, PUBLIC_ROUTES, TopPage } from './route.definitions';
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

const SwitchRouter: React.FC<SwitchRouterProps> = ({ loginedIn }) => {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <Suspense>
            <MainLayout />
          </Suspense>
        }
      >
        {PUBLIC_ROUTES.map(({ path, component: Component }) => (
          <Route
            key={path}
            path={path}
            element={
              <Suspense fallback={<Spinner />}>
                {path !== LOGIN_PATH ? (
                  <PublicLayOut>
                    <Component />
                  </PublicLayOut>
                ) : (
                  <BlankLayOut>
                    <Component />
                  </BlankLayOut>
                )}
              </Suspense>
            }
          />
        ))}
        {loginedIn ? (
          <>
            {PRIVATE_ROUTES.map(({ id, routes, template: LayoutTemplate }) =>
              routes.map(({ component: Component, path }) => (
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
              ))
            )}
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
                  <PublicLayOut>
                    <LandingPage />
                  </PublicLayOut>
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
