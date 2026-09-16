import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, takeLatest } from 'redux-saga/effects';

import { showToast } from './slice';
import type { ToastPayload } from './type';

function* handleToast(action: PayloadAction<ToastPayload>) {
  yield call(showToast, action.payload);
}

function* watchToast() {
  yield takeLatest(showToast.type, handleToast);
}

export function* toastSaga() {
  yield all([fork(watchToast)]);
}
