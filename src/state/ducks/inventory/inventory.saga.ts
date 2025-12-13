import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { InventoryResponse } from '@/@types/inventory';
import { inventoryRequestApi } from '@/apis/inventory/inventory';
import type { ApiError } from '@/apis/type';

import { inventoriesFailure, inventoriesRequest, inventoriesSuccess } from './slice';

function* handleInventoryRequest() {
  try {
    const res: InventoryResponse = yield call(inventoryRequestApi);
    yield put(inventoriesSuccess(res.inventories));
  } catch (error) {
    yield put(inventoriesFailure(error as ApiError));
  }
}

function* watchInventory() {
  yield takeLatest(inventoriesRequest.type, handleInventoryRequest);
}

export function* inventorySaga() {
  yield all([fork(watchInventory)]);
}
