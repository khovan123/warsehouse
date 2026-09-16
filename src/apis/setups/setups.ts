import { SETUPS_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const setupsApi = async () => await getClient().get(SETUPS_REQUEST_PATH);
