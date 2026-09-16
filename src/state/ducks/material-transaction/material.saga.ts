import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import { fetchMaterialTransactionApi } from '@/apis/material-transaction/material-transaction';
import type { ApiError, Unwrap } from '@/apis/type';
import type { InventoryType } from '@/utils/constants';

import type { FetchMaterialTransactionResponse } from '../../../@types/material-transaction';

import {
  fetchMaterialTransactionFailure,
  fetchMaterialTransactionRequest,
  fetchMaterialTransactionSuccess,
} from './slice';

function* fetchMaterialTransactionFlow(
  action: PayloadAction<{ inventoryType?: InventoryType; warehouseId?: string }>
) {
  try {
    const { inventoryType, warehouseId } = action.payload;
    const res: Unwrap<FetchMaterialTransactionResponse> = yield call(
      fetchMaterialTransactionApi,
      inventoryType,
      warehouseId
    );
    yield put(
      fetchMaterialTransactionSuccess({
        data: {
          materialTransactions: res.materialTransactions,
        },
      })
    );
  } catch (error) {
    yield put(fetchMaterialTransactionFailure({ error: error as ApiError }));
  }
}

function* watchMaterialTransactionFlows() {
  yield takeLatest(fetchMaterialTransactionRequest.type, fetchMaterialTransactionFlow);
}

export function* materialTransactionSaga() {
  yield all([fork(watchMaterialTransactionFlows)]);
}
