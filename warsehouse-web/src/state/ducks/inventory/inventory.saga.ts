import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchInventoryResponse } from '@/@types/inventory';
import { fetchInventoryApi } from '@/apis/inventory/inventory';
import type { ApiError, Unwrap } from '@/apis/type';

import { fetchInventoryFailure, fetchInventoryRequest, fetchInventorySuccess } from './slice';

function* fetchInventoryFlow() {
  try {
    const res: Unwrap<FetchInventoryResponse> = yield call(fetchInventoryApi);
    yield put(fetchInventorySuccess(res.inventories));
  } catch (error) {
    yield put(fetchInventoryFailure(error as ApiError));
  }
}

function* watchInventoryFlows() {
  yield takeLatest(fetchInventoryRequest.type, fetchInventoryFlow);
}

export function* inventorySaga() {
  yield all([fork(watchInventoryFlows)]);
}
