import type { ApiError } from '@/apis/type';

export type Inventory = {
  id: string;
  document: string;
  line: number;
  type: string;
  movementDate: string;
  productId: string;
  productName: string;
  warehouseId: string;
  warehouseName: string;
  binId: string;
  binName: string;
  qty: number;
  uom: string;
  cost: number;
  bpartnerId: string;
  businessPartnerName: string;
};

export type InventoryState = {
  loading: boolean;
  data: {
    inventories: Inventory[] | [];
  };
  error: ApiError | null;
};

export const INIT_INVENTORY_STATE: InventoryState = {
  loading: false,
  data: {
    inventories: [],
  },
  error: null,
};
