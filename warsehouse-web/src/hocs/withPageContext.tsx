import type { ComponentType } from 'react';

import { PageContextProvider } from '@/context/page-context';

export const WithPageContext = <P extends object>(Component: ComponentType<P>) => {
  const WrappedComponent = (props: P) => {
    return (
      <PageContextProvider>
        <Component {...props} />
      </PageContextProvider>
    );
  };

  WrappedComponent.displayName = `(${Component.displayName || Component.name || 'Component'})`;

  return WrappedComponent;
};
