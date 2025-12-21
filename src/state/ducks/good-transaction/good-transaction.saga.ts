import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { GoodTransactionResponse } from '@/@types/good-transaction';
import { fetchGoodTransactionApi } from '@/apis/good-transaction/good-transaction';
import type { ApiError } from '@/apis/type';

import {
  fetchGoodTransactionFailure,
  fetchGoodTransactionRequest,
  fetchGoodTransactionSuccess,
} from './slice';

function* fetchGoodTransactionFlow() {
  try {
    const res: GoodTransactionResponse = yield call(fetchGoodTransactionApi);
    yield put(fetchGoodTransactionSuccess({ data: { goodTransactions: res.goodTransactions } }));
  } catch (error) {
    yield put(fetchGoodTransactionFailure({ error: error as ApiError }));
  }
}

function* watchGoodTransactionFlows() {
  yield takeLatest(fetchGoodTransactionRequest.type, fetchGoodTransactionFlow);
}

export function* goodTransactionSaga() {
  yield all([fork(watchGoodTransactionFlows)]);
}
