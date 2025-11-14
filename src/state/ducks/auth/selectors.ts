import { useSelector } from 'react-redux';

import type { GlobalState } from '../../store';

export const useAuthSelector = () => {
  const auth = useSelector((state: GlobalState) => state.auth);
  return auth;
};
