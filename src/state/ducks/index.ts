import { all } from 'redux-saga/effects';

import { authSaga } from './auth/auth.saga';
import { inventorySaga } from './inventory/inventory.saga';
import { productSaga } from './product/product.saga';
import { setupsWarehouseSaga } from './setups-warehouse/setups.saga';
import { toastSaga } from './toast/toast.saga';

export default function* rootSaga() {
  yield all([authSaga(), toastSaga(), productSaga(), setupsWarehouseSaga(), inventorySaga()]);
}
