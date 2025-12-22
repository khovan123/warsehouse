import { PARETO_REQUEST_PATH } from '@/apis/constants';
import { getClient } from '@/apis/request';

export const paretoRequestApi = async () => await getClient().get(PARETO_REQUEST_PATH);
