import { useSelector } from 'react-redux';
import { BrowserRouter } from 'react-router';
import { ToastContainer } from 'react-toastify';

import Router from './routers/Router';
import type { GlobalState } from './state/store';

function App() {
  const { logined } = useSelector((state: GlobalState) => state.auth);
  return (
    <BrowserRouter>
      <Router loginedIn={!!logined} />
      <ToastContainer />
    </BrowserRouter>
  );
}

export default App;
