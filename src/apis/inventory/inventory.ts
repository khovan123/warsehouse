import { INVENTORY_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const inventoryRequestApi = async () => await getClient().get(INVENTORY_REQUEST_PATH);
