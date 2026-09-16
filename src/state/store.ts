import { combineReducers, configureStore } from '@reduxjs/toolkit';
import createSagaMiddleware from 'redux-saga';

import rootSaga from './ducks';
import authReducer from './ducks/auth/slice';
import toastReducer from './ducks/toast/slice';

const sagaMiddleware = createSagaMiddleware();

const store = configureStore({
  reducer: combineReducers({ auth: authReducer, toast: toastReducer }),
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ thunk: false }).concat(sagaMiddleware),
});

sagaMiddleware.run(rootSaga);

export default store;
export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
