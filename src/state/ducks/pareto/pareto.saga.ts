import { all, call, fork, put, takeLatest } from 'redux-saga/effects';

import type { FetchParetoResponse } from '@/@types/pareto';
import { fetchParetoRequestApi } from '@/apis/pareto/pareto';
import type { ApiError } from '@/apis/type';

import { fetchParetoFailure, fetchParetoRequest, fetchParetoSuccess } from './slice';

function* fetchParetoFlow() {
  try {
    const res: FetchParetoResponse = yield call(fetchParetoRequestApi);
    const paretos = res.paretos;
    const classifications = res.classifications;
    yield put(
      fetchParetoSuccess({
        data: {
          paretos,
          classifications,
        },
      })
    );
  } catch (error) {
    yield put(fetchParetoFailure({ error: error as ApiError }));
  }
}

function* watchParetoFlows() {
  yield takeLatest(fetchParetoRequest.type, fetchParetoFlow);
}

export function* paretoSaga() {
  yield all([fork(watchParetoFlows)]);
}
