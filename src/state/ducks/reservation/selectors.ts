import { useSelector } from 'react-redux';

import type { RootState } from '@/state/store';

export const useReservationSelectors = () => {
  const reservationSelectors = useSelector((state: RootState) => state.reservation);
  return reservationSelectors.data;
};
