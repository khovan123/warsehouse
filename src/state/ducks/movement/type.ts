import type { ApiError } from '@/apis/type';

export type Movement = {
  id: string;
  docNo: string;
  movementDate: string;
  fromWarehouse: string;
  toWarehouse: string;
  status: string;
  reason: string;
  createdBy: string;
  lines: MovementLine[] | [];
};

export type MovementLine = {
  productId: string;
  qty: number;
  fromBin: string;
  toBin: string;
};

export type MovementState = {
  loading: boolean;
  data: {
    movements: Movement[] | [];
  };
  error: ApiError | null;
};

export const INIT_MOVEMENT_STATE: MovementState = {
  loading: false,
  data: {
    movements: [],
  },
  error: null,
};
