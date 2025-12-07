import { PRODUCT_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const productRequestApi = async () => await getClient().get(PRODUCT_REQUEST_PATH);
