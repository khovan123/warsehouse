import type { Inventory } from '@/state/ducks/inventory/type';

export type FetchInventoryResponse = {
  inventories: Inventory[];
};
