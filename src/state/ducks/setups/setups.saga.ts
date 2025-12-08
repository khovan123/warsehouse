import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { SetupsResponse } from '@/@types/setups';
import { setupsApi } from '@/apis/setups/setups';
import type { ApiError } from '@/apis/type';

import { setupsFailure, setupsRequest, setupsSuccess } from './slice';

function* handleSetupsRequest() {
  try {
    const res: SetupsResponse = yield call(setupsApi);
    yield put(setupsSuccess({ warehouses: res.warehouses, bins: res.bins }));
  } catch (error) {
    yield put(setupsFailure(error as ApiError));
  }
}

function* watchSetups() {
  yield takeLatest(setupsRequest.type, handleSetupsRequest);
}

export function* setupsSaga() {
  yield all([fork(watchSetups)]);
}
