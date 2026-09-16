import type { PayloadAction } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { all, call, fork, takeLatest } from 'redux-saga/effects';

import { showToast } from './slice';
import type { ToastPayload } from './type';

function* handleToast(action: PayloadAction<ToastPayload>) {
  const { type, message, id } = action.payload;

  const fn =
    type === 'success'
      ? toast.success
      : type === 'error'
        ? toast.error
        : type === 'info'
          ? toast.info
          : toast.warn;
  yield call(fn, message, { toastId: id });
}

function* watchToast() {
  yield takeLatest(showToast.type, handleToast);
}

export function* toastSaga() {
  yield all([fork(watchToast)]);
}
