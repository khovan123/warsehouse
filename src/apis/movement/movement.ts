import type { Period, InventoryType } from '@/utils/constants';

import { MOVEMENT_REPORT_PATH, MOVEMENT_REQUEST_PATH, MOVEMENT_SUMMARY_PATH } from '../constants';
import { getClient } from '../request';

export const fetchMovementApi = () => getClient().get(MOVEMENT_REQUEST_PATH);

export const fetchMovementReportApi = (inventoryType?: InventoryType, period?: Period) => {
  const type = inventoryType != -1 ? inventoryType : {};
  return getClient().get(MOVEMENT_REPORT_PATH, { params: { type, period } });
};

export const fetchMovementSummaryApi = (period: Period) =>
  getClient().get(MOVEMENT_SUMMARY_PATH, { params: { period } });
