import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import {
  fetchMovementApi,
  fetchMovementReportApi,
  fetchMovementSummaryApi,
} from '@/apis/movement/movement';
import type { ApiError, Unwrap } from '@/apis/type';
import type { Period } from '@/utils/constants';

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
  fetchMovementSummaryFailure,
  fetchMovementSummaryRequest,
  fetchMovementSummarySuccess,
} from './slice';

function* fetchMovementFlow() {
  try {
    const res: Unwrap<FetchMovementResponse> = yield call(fetchMovementApi);
    yield put(
      fetchMovementSuccess({
        data: { movements: res.movements, movementReports: [], movementSummaries: [] },
      })
    );
  } catch (error) {
    yield put(fetchMovementFailure({ error: error as ApiError }));
  }
}

function* fetchMovementReportFlow() {
  try {
    const res: Unwrap<FetchMovementReportResponse> = yield call(fetchMovementReportApi);
    yield put(
      fetchMovementReportSuccess({
        data: { movements: [], movementReports: res.movementReports, movementSummaries: [] },
      })
    );
  } catch (error) {
    yield put(fetchMovementReportFailure({ error: error as ApiError }));
  }
}

function* fetchMovementSummaryFlow(action: PayloadAction<{ period: Period }>) {
  try {
    const res: Unwrap<FetchMovementSummaryResponse> = yield call(
      fetchMovementSummaryApi,
      action.payload.period
    );
    yield put(
      fetchMovementSummarySuccess({
        data: {
          movements: [],
          movementReports: [],
          movementSummaries: res.movementSummaries,
        },
      })
    );
  } catch (error) {
    yield put(fetchMovementSummaryFailure({ error: error as ApiError }));
  }
}

function* watchMovementFlows() {
  yield takeLatest(fetchMovementRequest.type, fetchMovementFlow);
  yield takeLatest(fetchMovementReportRequest.type, fetchMovementReportFlow);
  yield takeLatest(fetchMovementSummaryRequest.type, fetchMovementSummaryFlow);
}

export function* movementSaga() {
  yield all([fork(watchMovementFlows)]);
}
