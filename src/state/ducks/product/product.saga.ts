import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { ProductResponse } from '@/@types/product';
import { productRequestApi } from '@/apis/product/product';
import type { ApiError } from '@/apis/type';

import { productFailure, productRequest, productSuccess } from './slice';

function* handleProductRequest() {
  try {
    const res: ProductResponse = yield call(productRequestApi);
    yield put(productSuccess(res.products));
  } catch (error) {
    yield put(productFailure(error as ApiError));
  }
}

function* watchProduct() {
  yield takeLatest(productRequest.type, handleProductRequest);
}

export function* productSaga() {
  yield all([fork(watchProduct)]);
}
