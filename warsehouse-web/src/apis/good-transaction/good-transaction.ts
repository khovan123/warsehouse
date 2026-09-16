import { GOODTRANSACTION_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const fetchGoodTransactionApi = () => getClient().get(GOODTRANSACTION_REQUEST_PATH);
