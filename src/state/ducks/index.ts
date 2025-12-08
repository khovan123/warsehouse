import { all } from 'redux-saga/effects';

import { authSaga } from './auth/auth.saga';
import { productSaga } from './product/product.saga';
import { setupsSaga } from './setups/setups.saga';
import { toastSaga } from './toast/toast.saga';

export default function* rootSaga() {
  yield all([authSaga(), toastSaga(), productSaga(), setupsSaga()]);
}
