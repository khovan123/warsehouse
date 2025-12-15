import { SETUPS_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const fetchSetupsWarehouseApi = async () => await getClient().get(SETUPS_REQUEST_PATH);
