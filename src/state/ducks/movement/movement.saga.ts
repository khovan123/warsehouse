import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import {
  fetchMovementApi,
  fetchMovementReportApi,
  fetchMovementSummaryApi,
} from '@/apis/movement/movement';
import type { ApiError, Unwrap } from '@/apis/type';
import type { InventoryType, Period } from '@/utils/constants';

import type {
  FetchMovementReportResponse,
  FetchMovementResponse,
  FetchMovementSummaryResponse,
} from './../../../@types/movement';
import {
  fetchMovementFailure,
  fetchMovementReportFailure,
  fetchMovementReportRequest,
  fetchMovementReportSuccess,
  fetchMovementRequest,
  fetchMovementSuccess,
} from './slice';

function* fetchMovementFlow() {
  try {
    const res: Unwrap<FetchMovementResponse> = yield call(fetchMovementApi);
    yield put(
      fetchMovementSuccess({
        data: {
          movements: res.movements,
          movementReports: [],
          movementSummaries: [],
        },
      })
    );
  } catch (error) {
    yield put(fetchMovementFailure({ error: error as ApiError }));
  }
}

function* fetchMovementReportFlow(
  action: PayloadAction<{ inventoryType?: InventoryType; period: Period }>
) {
  try {
    const { inventoryType, period } = action.payload;
    const res_summary: Unwrap<FetchMovementSummaryResponse> = yield call(
      fetchMovementSummaryApi,
      period
    );

    const res_report: Unwrap<FetchMovementReportResponse> = yield call(
      fetchMovementReportApi,
      inventoryType,
      period
    );
    yield put(
      fetchMovementReportSuccess({
        movementSummaries: res_summary.movementSummaries,
        movementReports: res_report.movementReports,
      })
    );
  } catch (error) {
    yield put(fetchMovementReportFailure({ error: error as ApiError }));
  }
}

function* watchMovementFlows() {
  yield takeLatest(fetchMovementRequest.type, fetchMovementFlow);
  yield takeLatest(fetchMovementReportRequest.type, fetchMovementReportFlow);
}

export function* movementSaga() {
  yield all([fork(watchMovementFlows)]);
}
