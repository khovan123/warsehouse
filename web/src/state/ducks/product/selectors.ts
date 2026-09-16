import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useProductSelector = () => {
  const productSelector = useSelector((state: RootState) => state.product);
  return productSelector;
};
