import { STOCK_REQUEST_PATH } from '@/apis/constants';
import { getClient } from '@/apis/request';

export const stockRequestApi = async () => await getClient().get(STOCK_REQUEST_PATH);
