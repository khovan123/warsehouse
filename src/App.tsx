import { useSelector } from 'react-redux';
import { BrowserRouter } from 'react-router';
import { ToastContainer } from 'react-toastify';
import { PersistGate } from 'redux-persist/integration/react';

import Router from './routers/Router';
import { persistor, type GlobalState } from './state/store';

function App() {
  const { logined } = useSelector((state: GlobalState) => state.auth);
  return (
    <PersistGate loading={null} persistor={persistor}>
      <BrowserRouter>
        <Router loginedIn={!!logined} />
        <ToastContainer />
      </BrowserRouter>
    </PersistGate>
  );
}

export default App;
