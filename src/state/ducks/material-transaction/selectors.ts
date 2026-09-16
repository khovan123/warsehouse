import { useSelector } from 'react-redux';

import type { RootState } from '../../store';

export const useMaterialTransactionSelector = () => {
  const materialTransactionSelector = useSelector((state: RootState) => state.materialTransaction);
  return materialTransactionSelector;
};
