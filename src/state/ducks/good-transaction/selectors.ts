import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useGoodTransactionSelector = () => {
  const goodTransactionSelector = useSelector((state: RootState) => state.goodTransaction);
  return goodTransactionSelector;
};
