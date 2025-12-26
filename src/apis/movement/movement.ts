import type { Period } from '@/utils/constants';
import { MOVEMENT_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const fetchMovementApi = () => getClient().get(MOVEMENT_REQUEST_PATH);

export const fetchMovementReportApi = () => getClient().get(`${MOVEMENT_REQUEST_PATH}/report`);

export const fetchMovementSummaryApi = (period: Period) =>
  getClient().get(`${MOVEMENT_REQUEST_PATH}/summary`, { params: { period } });
