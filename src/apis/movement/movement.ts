import { MOVEMENT_REPORT_PATH, MOVEMENT_REQUEST_PATH, MOVEMENT_SUMMARY_PATH } from '../constants';
import { getClient } from '../request';

export const fetchMovementApi = () => getClient().get(MOVEMENT_REQUEST_PATH);

export const fetchMovementReportApi = () => getClient().get(MOVEMENT_REPORT_PATH);

export const fetchMovementSummaryApi = (period: number | string) =>
  getClient().get(MOVEMENT_SUMMARY_PATH, { params: { period } });
