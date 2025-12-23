import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import { INIT_PARETO_STATE, type ParetoState } from './type';

export const paretoSlice = createSlice({
  name: 'pareto',
  initialState: INIT_PARETO_STATE,
  reducers: {
    fetchParetoRequest: (state) => {
      state.loading = true;
    },
    fetchParetoSuccess: (state, action: PayloadAction<Pick<ParetoState, 'data'>>) => {
      state.data.paretos = action.payload.data.paretos;
      state.data.classifications = action.payload.data.classifications;
      state.loading = false;
    },
    fetchParetoFailure: (state, action: PayloadAction<Pick<ParetoState, 'error'>>) => {
      state.error = action.payload.error;
      state.loading = false;
    },
  },
});

export const { fetchParetoRequest, fetchParetoSuccess, fetchParetoFailure } = paretoSlice.actions;
export default paretoSlice.reducer;
