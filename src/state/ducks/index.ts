import { all } from 'redux-saga/effects';

import { authSaga } from './auth/auth.saga';

export default function* rootSaga() {
  yield all([authSaga()]);
}
