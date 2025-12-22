import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { ParetoResponse } from '@/@types/pareto';
import { paretoRequestApi } from '@/apis/pareto/pareto';
import type { ApiError } from '@/apis/type';

import { paretoFailure, paretoRequest, paretoSuccess } from './slice';

function* handleParetoRequest() {
  try {
    const res: ParetoResponse = yield call(paretoRequestApi);

    const paretos = res.paretos || [];
    const classifications = res.classifications || [];

    yield put(
      paretoSuccess({
        paretos,
        classifications,
      })
    );
  } catch (error) {
    yield put(paretoFailure(error as ApiError));
  }
}

function* watchPareto() {
  yield takeLatest(paretoRequest.type, handleParetoRequest);
}

export function* paretoSaga() {
  yield all([fork(watchPareto)]);
}
