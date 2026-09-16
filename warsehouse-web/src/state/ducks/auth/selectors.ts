import { useSelector } from 'react-redux';

import type { GlobalState } from '@/state/store';

export const useAuthSelector = () => {
  const auth = useSelector((state: GlobalState) => state.auth);
  return auth;
};

export const useUserSelector = () => {
  const user = useSelector((state: GlobalState) => state.auth.data?.user);
  return user;
};
