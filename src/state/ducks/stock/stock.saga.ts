import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchStockResponse } from '@/@types/stock';
import { stockRequestApi } from '@/apis/stock/stock';
import type { ApiError, Unwrap } from '@/apis/type';

import { stockFailure, stockRequest, stockSuccess } from './slice';

function* handleStockRequest() {
  try {
    const res: Unwrap<FetchStockResponse> = yield call(stockRequestApi);
    yield put(stockSuccess(res.stocks));
  } catch (error) {
    yield put(stockFailure(error as ApiError));
  }
}

function* watchStock() {
  yield takeLatest(stockRequest.type, handleStockRequest);
}

export function* stockSaga() {
  yield all([fork(watchStock)]);
}
