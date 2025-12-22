import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_PARETO_STATE, type Pareto, type ParetoClassification } from './type';

type ParetoPayload = {
  paretos: Pareto[];
  classifications: ParetoClassification[];
};

export const paretoSlice = createSlice({
  name: 'pareto',
  initialState: INIT_PARETO_STATE,
  reducers: {
    paretoRequest: (state) => {
      state.loading = true;
    },
    paretoSuccess: (state, action: PayloadAction<ParetoPayload>) => {
      state.data.paretos = action.payload.paretos;
      state.data.classifications = action.payload.classifications;
      state.loading = false;
    },
    paretoFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { paretoRequest, paretoSuccess, paretoFailure } = paretoSlice.actions;
export default paretoSlice.reducer;
