import type { Reservation } from '@/state/ducks/reservation/type';

export type FetchReservationReponse = {
  reservations: Reservation[] | [];
};
