import React from 'react';
import { ErrorBoundary } from 'react-error-boundary';
import { useSelector } from 'react-redux';
import { BrowserRouter } from 'react-router';
import { toast, ToastContainer } from 'react-toastify';
import { PersistGate } from 'redux-persist/integration/react';

import { ErrorFallback } from '@/components/organisms/ErrorFallBack/ErrorFallBack';
import Router from '@/routers/Router';
import { persistor, type GlobalState } from '@/state/store';

function App() {
  const { logined } = useSelector((state: GlobalState) => state.auth);
  return (
    <PersistGate loading={null} persistor={persistor}>
      <BrowserRouter>
        <ErrorBoundary
          FallbackComponent={ErrorFallback}
          onReset={() => {}}
          onError={(error: Error, info: React.ErrorInfo) => {
            toast.dismiss();
            console.error(error, info);
          }}
        >
          <Router loginedIn={!!logined} />
        </ErrorBoundary>
        <ToastContainer />
      </BrowserRouter>
    </PersistGate>
  );
}

export default App;
