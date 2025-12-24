import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import { fetchMovementApi } from '@/apis/movement/movement';
import type { ApiError, Unwrap } from '@/apis/type';

import type { FetchMovementResponse } from './../../../@types/movement';
import { fetchMovementFailure, fetchMovementRequest, fetchMovementSuccess } from './slice';

function* fetchMovementFlow() {
  try {
    const res: Unwrap<FetchMovementResponse> = yield call(fetchMovementApi);
    yield put(fetchMovementSuccess({ data: { movements: res.movements } }));
  } catch (error) {
    yield put(fetchMovementFailure({ error: error as ApiError }));
  }
}

function* watchMovementFlows() {
  yield takeLatest(fetchMovementRequest.type, fetchMovementFlow);
}

export function* movementSaga() {
  yield all([fork(watchMovementFlows)]);
}
