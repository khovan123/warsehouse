import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useStockSelector = () => {
  const stockSelector = useSelector((state: RootState) => state.stock);
  return stockSelector;
};
