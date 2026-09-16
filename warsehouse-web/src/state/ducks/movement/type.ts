import type { ApiError } from '@/apis/type';
import type { InventoryType } from '@/utils/constants';

export type Movement = {
  id: string;
  docNo: string;
  movementDate: string;
  fromWarehouse: string;
  fromWarehouseName: string;
  toWarehouse: string;
  toWarehouseName: string;
  status: string;
  reason: string;
  createdBy: string;
  lines: MovementLine[] | [];
};

export type MovementLine = {
  productId: string;
  productName: string;
  qty: number;
  fromBin: string;
  fromBinName: string;
  toBin: string;
  toBinName: string;
};

export type MovementReport = {
  docNo: string;
  movementDate: string;
  product: string;
  fromWarehouse: string;
  toWarehouse: string;
  bin: string;
  type: InventoryType;
  qty: number;
};

export type MovementSummary = {
  period: string;
  outbound: number;
  inbound: number;
  total: number;
};

export type MovementState = {
  loading: boolean;
  data: {
    movements: Movement[] | [];
    movementReports: MovementReport[] | [];
    movementSummaries: MovementSummary[] | [];
  };
  error: ApiError | null;
};

export type MovementReportSuccessPayload = {
  movementReports?: MovementReport[] | [];
  movementSummaries?: MovementSummary[] | [];
};

export const INIT_MOVEMENT_STATE: MovementState = {
  loading: false,
  data: {
    movements: [],
    movementReports: [],
    movementSummaries: [],
  },
  error: null,
};
