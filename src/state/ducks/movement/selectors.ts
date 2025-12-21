import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useMovementSelectors = () => {
  const movementSelector = useSelector((state: RootState) => state.movement);
  return movementSelector.data;
};
