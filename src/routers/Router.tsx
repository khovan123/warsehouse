import React, { Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router';

import SpinderCustom from '@/components/atoms/SpinderCustom/SpinderCustom';
import BlankLayOut from '@/components/templates/BlankLayout/BlankLayOut';
import MainLayout from '@/components/templates/MainLayout/MainLayout';
import PublicLayOut from '@/components/templates/PublicLayout/PublicLayOut';

import { LOGIN_PATH, PRODUCT_MANAGEMENT_PATH } from './route.constants';
import { PRIVATE_ROUTES, PUBLIC_ROUTES } from './route.definitions';
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
              <Suspense fallback={<SpinderCustom />}>
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
            <Route path="/" element={<Navigate to={PRODUCT_MANAGEMENT_PATH} replace />} />

            {PRIVATE_ROUTES.map(({ id, routes, template: LayoutTemplate }) => (
              <Route
                element={
                  <Suspense fallback={<SpinderCustom />}>
                    <LayoutTemplate />
                  </Suspense>
                }
              >
                <>
                  {routes.map(({ component: Component, path }) => (
                    <Route
                      key={`${id}-${path}`}
                      path={path}
                      element={
                        <Suspense fallback={<SpinderCustom />}>
                          {/* <LayoutTemplate> */}
                          <Component />
                          {/* </LayoutTemplate> */}
                        </Suspense>
                      }
                    />
                  ))}
                </>
              </Route>
            ))}

            <Route
              path="/error"
              element={
                <Suspense fallback={<SpinderCustom />}>
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
