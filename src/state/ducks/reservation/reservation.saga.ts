import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchReservationReponse } from '@/@types/reservation';
import { fetchReservationApi } from '@/apis/reservation/reservation';
import type { ApiError } from '@/apis/type';

import { fetchReservationFailure, fetchReservationRequest, fetchReservationSuccess } from './slice';

function* fetchReservationFlow() {
  try {
    const res: FetchReservationReponse = yield call(fetchReservationApi);
    yield put(fetchReservationSuccess({ data: { reservations: res.reservations } }));
  } catch (error) {
    yield put(fetchReservationFailure({ error: error as ApiError }));
  }
}

function* watchReservationFlows() {
  yield takeLatest(fetchReservationRequest.type, fetchReservationFlow);
}

export function* reservationSaga() {
  yield all([fork(watchReservationFlows)]);
}
