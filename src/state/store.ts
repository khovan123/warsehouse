import { combineReducers, configureStore, type Reducer } from '@reduxjs/toolkit';
import { persistReducer, persistStore } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import createSagaMiddleware from 'redux-saga';

import rootSaga from './ducks';
import { authPersistFilter } from './ducks/auth/persist-filter';
import authReducer from './ducks/auth/slice';
import productReducer from './ducks/product/slice';
import setupsReducer from './ducks/setups/slice';
import toastReducer from './ducks/toast/slice';

const rootReducer = combineReducers({
  auth: authReducer,
  toast: toastReducer,
  product: productReducer,
  setups: setupsReducer,
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
