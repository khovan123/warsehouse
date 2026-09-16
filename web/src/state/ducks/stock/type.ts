import type { ApiError } from '@/apis/type';

export type Stock = {
  id: string;
  productId: string;
  warehouseId: string;
  binId: string;
  categoryId: string;
  product: string;
  warehouse: string;
  bin: string;
  category: string;
  onHand: number;
  reserved: number;
  available: number;
  averageCost: number;
  inventoryValue: number;
};

export type StockState = {
  data: {
    stocks: Stock[] | null;
  };
  loading: boolean;
  error: ApiError | null;
};

export const INIT_STOCK_STATE: StockState = {
  data: {
    stocks: null,
  },
  loading: false,
  error: null,
};
