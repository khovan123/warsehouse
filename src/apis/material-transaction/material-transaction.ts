import type { InventoryType } from '@/utils/constants';

import { MOVEMENT_MATERIAL_TRANSACTION_PATH } from '../constants';
import { getClient } from '../request';

export const fetchMaterialTransactionApi = (
  inventoryType?: InventoryType,
  warehouseId?: string
) => {
  const type = inventoryType != -1 ? inventoryType : {};
  const warehouse = warehouseId !== '-1' ? warehouseId : {};
  return getClient().get(MOVEMENT_MATERIAL_TRANSACTION_PATH, {
    params: { type, warehouseId: warehouse },
  });
};
