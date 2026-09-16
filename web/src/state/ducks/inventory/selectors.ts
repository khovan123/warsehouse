import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useInventorySelector = () => {
  const inventorySelector = useSelector((state: RootState) => state.inventory);
  return inventorySelector;
};
