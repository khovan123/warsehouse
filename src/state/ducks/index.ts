import { all } from 'redux-saga/effects';

import { authSaga } from './auth/auth.saga';
import { goodTransactionSaga } from './good-transaction/good-transaction.saga';
import { inventorySaga } from './inventory/inventory.saga';
import { movementSaga } from './movement/movement.saga';
import { productSaga } from './product/product.saga';
import { setupsWarehouseSaga } from './setups-warehouse/setups.saga';
import { stockSaga } from './stock/stock.saga';
import { toastSaga } from './toast/toast.saga';

export default function* rootSaga() {
  yield all([
    authSaga(),
    toastSaga(),
    productSaga(),
    stockSaga(),
    setupsWarehouseSaga(),
    inventorySaga(),
    movementSaga(),
    goodTransactionSaga(),
  ]);
}
