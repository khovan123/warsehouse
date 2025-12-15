import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchSetupsWarehouseResponse } from '@/@types/setups';
import { fetchSetupsWarehouseApi } from '@/apis/setups/setups';
import type { ApiError } from '@/apis/type';

import {
  fetchSetupsWarehouseFailure,
  fetchSetupsWarehouseRequest,
  fetchSetupsWarehouseSuccess,
} from './slice';

function* fetchSetupsWarehouseFlow() {
  try {
    const res: FetchSetupsWarehouseResponse = yield call(fetchSetupsWarehouseApi);
    yield put(fetchSetupsWarehouseSuccess({ warehouses: res.warehouses, bins: res.bins }));
  } catch (error) {
    yield put(fetchSetupsWarehouseFailure(error as ApiError));
  }
}

function* watchSetupsWarehouseFlows() {
  yield takeLatest(fetchSetupsWarehouseRequest.type, fetchSetupsWarehouseFlow);
}

export function* setupsWarehouseSaga() {
  yield all([fork(watchSetupsWarehouseFlows)]);
}
