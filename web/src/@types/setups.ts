import type { Bin, Warehouse } from '@/state/ducks/setups-warehouse/type';

export type FetchSetupsWarehouseResponse = {
  warehouses: Warehouse[];
  bins: Bin[];
};
