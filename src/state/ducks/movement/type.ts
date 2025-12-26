import type { ApiError } from '@/apis/type';
import type { MovementType } from '@/utils/constants';

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

export type MovementReport = {
  docNo: string;
  movementDate: string;
  product: string;
  fromWarehouse: string;
  toWarehouse: string;
  bin: string;
  type: MovementType;
  qty: number;
};

export type MovementSummary = {
  period: string;
  negativeQty: number;
  positiveQty: number;
  totalQty: number;
};

export type MovementState = {
  loading: boolean;
  reportLoading: boolean;
  summaryLoading: boolean;
  data: {
    movements: Movement[] | [];
    movementReports: MovementReport[] | [];
    movementSummaries: MovementSummary[] | [];
  };
  error: ApiError | null;
};

export const INIT_MOVEMENT_STATE: MovementState = {
  loading: false,
  reportLoading: false,
  summaryLoading: false,
  data: {
    movements: [],
    movementReports: [],
    movementSummaries: [],
  },
  error: null,
};
