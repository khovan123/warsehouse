import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useMovementSelector = () => {
  const movementSelector = useSelector((state: RootState) => state.movement);
  return movementSelector;
};
