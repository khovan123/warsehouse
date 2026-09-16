import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, takeLatest } from 'redux-saga/effects';

import { showToast } from './slice';
import type { ToastPayload } from './type';

function* showToastFlow(action: PayloadAction<ToastPayload>) {
  yield call(showToast, action.payload);
}

function* watchToastFlows() {
  yield takeLatest(showToast.type, showToastFlow);
}

export function* toastSaga() {
  yield all([fork(watchToastFlows)]);
}
