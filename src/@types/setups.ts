import type { Bin, Warehouse } from '@/state/ducks/setups/type';

export type SetupsResponse = {
  warehouses: Warehouse[];
  bins: Bin[];
};
