import { combineReducers, configureStore, type Reducer } from '@reduxjs/toolkit';
import { persistReducer, persistStore } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import createSagaMiddleware from 'redux-saga';

import rootSaga from './ducks';
import { authPersistFilter } from './ducks/auth/persist-filter';
import authReducer from './ducks/auth/slice';
import goodTransactionReducer from './ducks/good-transaction/slice';
import inventoryReducer from './ducks/inventory/slice';
import materialTransactionReducer from './ducks/material-transaction/slice';
import movementReducer from './ducks/movement/slice';
import paretoReducer from './ducks/pareto/slice';
import productReducer from './ducks/product/slice';
import reservationReducer from './ducks/reservation/slice';
import setupsReducer from './ducks/setups-warehouse/slice';
import stockReducer from './ducks/stock/slice';
import toastReducer from './ducks/toast/slice';

const rootReducer = combineReducers({
  auth: authReducer,
  toast: toastReducer,
  product: productReducer,
  setups: setupsReducer,
  inventory: inventoryReducer,
  stock: stockReducer,
  pareto: paretoReducer,
  movement: movementReducer,
  materialTransaction: materialTransactionReducer,
  goodTransaction: goodTransactionReducer,
  reservation: reservationReducer,
});

const persistConfig = {
  key: 'root',
  storage,
  whitelist: ['auth'],
  blacklist: ['toast'],
  transforms: [authPersistFilter],
};

const persistedReducer = persistReducer<RootState>(
  persistConfig,
  rootReducer as Reducer<RootState>
);

const sagaMiddleware = createSagaMiddleware();

const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ thunk: false, serializableCheck: false }).concat(sagaMiddleware),
});

sagaMiddleware.run(rootSaga);

export default store;
export const persistor = persistStore(store);
export type RootState = ReturnType<typeof rootReducer>;
export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
