import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchProductResponse } from '@/@types/product';
import { fetchProductApi } from '@/apis/product/product';
import type { ApiError, Unwrap } from '@/apis/type';

import { fetchProductFailure, fetchProductRequest, fetchProductSuccess } from './slice';

function* fetchProductFlow() {
  try {
    const res: Unwrap<FetchProductResponse> = yield call(fetchProductApi);
    yield put(fetchProductSuccess(res.products));
  } catch (error) {
    yield put(fetchProductFailure(error as ApiError));
  }
}

function* watchProductFlows() {
  yield takeLatest(fetchProductRequest.type, fetchProductFlow);
}

export function* productSaga() {
  yield all([fork(watchProductFlows)]);
}
