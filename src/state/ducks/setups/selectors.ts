import { useSelector } from 'react-redux';

import type { RootState } from './../../store';

export const useSetupsSelectopr = () => {
  const setupsSeletor = useSelector((state: RootState) => state.setups);
  return setupsSeletor;
};
