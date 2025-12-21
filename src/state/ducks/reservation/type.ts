import type { ApiError } from '@/apis/type';

export type Reservation = {
  id: string;
  reservationNo: string;
  rroductId: string;
  warehouseId: string;
  reservedQty: number;
  uom: string;
  promisedDate: Date;
  status: string;
  orderRef: string;
};

export type ReservationState = {
  loading: boolean;
  data: {
    reservations: Reservation[] | [];
  };
  error: ApiError | null;
};

export const INIT_RESERVATION_STATE: ReservationState = {
  loading: false,
  data: {
    reservations: [],
  },
  error: null,
};
