import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { LoginCredentials, LoginResponse } from '@/@types/auth';
import { loginApi } from '@/apis/auth/auth';
import type { ApiError } from '@/apis/type';
import { showToast } from '@/state/ducks/toast/slice';

import { loginFailure, loginRequest, loginSuccess } from './slice';

function* loginFlow(action: PayloadAction<LoginCredentials>) {
  try {
    // if (action.payload.username === 'openbravo' && action.payload.password === 'openbravo') {
    //   const user: LoginResponse = {
    //     id: 'openbravo',
    //     username: action.payload.username,
    //     password: action.payload.password,
    //   };
    //   yield put(loginSuccess(user));
    //   yield put(showToast({ type: 'success', message: 'Login successfully!' }));
    //   return;
    // }

    const user: LoginResponse = yield call(loginApi, action.payload);
    yield put(loginSuccess(user));
    yield put(showToast({ type: 'success', message: 'Login successfully!' }));
  } catch (error) {
    yield put(loginFailure(error as ApiError));
    yield put(showToast({ type: 'error', message: (error as ApiError).errorMessage }));
  }
}

function* watchAuthFlows() {
  yield takeLatest(loginRequest.type, loginFlow);
}

export function* authSaga() {
  yield all([fork(watchAuthFlows)]);
}
