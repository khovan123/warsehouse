import { PARETO_REQUEST_PATH } from '@/apis/constants';
import { getClient } from '@/apis/request';

export const fetchParetoRequestApi = async () => await getClient().get(PARETO_REQUEST_PATH);
