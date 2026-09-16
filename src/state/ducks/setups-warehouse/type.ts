import type { ApiError } from '@/apis/type';

export type Warehouse = {
  id: string;
  code: string;
  name: string;
  organization: string;
  isActive: boolean;
};
export type Bin = {
  id: string;
  warehouseId: string;
  code: string;
  description?: string;
};

export type Setups = {
  warehouses?: Warehouse[] | [];
  bins?: Bin[] | [];
};

export type SetupsState = {
  loading: boolean;
  data: Setups;
  error?: ApiError | null;
};

export const INIT_SETUPS_STATE: SetupsState = {
  loading: false,
  data: {
    warehouses: [],
    bins: [],
  },
  error: null,
};
