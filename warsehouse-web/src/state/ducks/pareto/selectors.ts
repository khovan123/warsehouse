import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useParetoSelector = () => {
  const paretoSelector = useSelector((state: RootState) => state.pareto);
  return paretoSelector;
};
