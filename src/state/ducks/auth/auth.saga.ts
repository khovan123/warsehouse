import type { PayloadAction } from '@reduxjs/toolkit';
import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { LoginCredentials, LoginResponse } from '../../../@types/auth';
import { loginApi } from '../../../apis/auth/auth';

import { loginFailure, loginRequest, loginSuccess } from './slice';

function* handleLogin(action: PayloadAction<LoginCredentials>) {
  try {
    const user: LoginResponse = yield call(loginApi, action.payload);
    yield put(loginSuccess(user));
  } catch (error) {
    yield put(loginFailure('Login failed'));
  }
}

function* watchAuth() {
  yield takeLatest(loginRequest.type, handleLogin);
}

export function* authSaga() {
  yield all([fork(watchAuth)]);
}
