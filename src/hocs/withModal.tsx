import type { ComponentType } from 'react';

import { ModalProvider } from '@/context/modal-context';

export const WithModal = <P extends object>(Component: ComponentType<P>) => {
  const WrappedComponent = (props: P) => {
    return (
      <ModalProvider>
        <Component {...props} />
      </ModalProvider>
    );
  };

  WrappedComponent.displayName = `(${Component.displayName || Component.name || 'Component'})`;

  return WrappedComponent;
};
