import type { ApiError } from '@/apis/type';

export type GoodTransaction = {
  id: string;
  docNo: string;
  warehouseId: string;
  warehouseName: string;
  countDate: Date;
  description?: string;
  status: string;
  createdBy: string;
  lines: GoodTransactionLine[] | [];
};

export type GoodTransactionLine = {
  productId: string;
  productName: string;
  expectedQty: number;
  countedQty: number;
  difference: number;
  binId: string;
  binName: string;
  uom: string;
};

export type GoodTransactionState = {
  loading: boolean;
  data: {
    goodTransactions: GoodTransaction[] | [];
  };
  error: ApiError | null;
};

export const INIT_GOODTRANSACTION_STATE: GoodTransactionState = {
  loading: false,
  data: {
    goodTransactions: [],
  },
  error: null,
};
