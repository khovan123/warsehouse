import type { ApiError } from '@/apis/type';

export type MaterialTransaction = {
  line: number;
  document: string;
  movementDate: string;
  product: string;
  fromWarehouse: string;
  bin: string;
  movementQty: number;
  uom: string;
  cost: number;
  type: string;
  businessPartner: string;
};

export type MaterialTransactionState = {
  loading: boolean;
  data: {
    materialTransactions: MaterialTransaction[] | [];
  };
  error: ApiError | null;
};

export const INIT_MATERIAL_TRANSACTION_STATE: MaterialTransactionState = {
  loading: false,
  data: {
    materialTransactions: [],
  },
  error: null,
};
