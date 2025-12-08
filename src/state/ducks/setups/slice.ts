import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_SETUPS_STATE, type Setups } from './type';

export const setupsSlice = createSlice({
  name: 'setups',
  initialState: INIT_SETUPS_STATE,
  reducers: {
    setupsRequest: (state) => {
      state.loading = true;
    },
    setupsSuccess: (state, action: PayloadAction<Setups>) => {
      state.loading = false;
      state.data.bins = action.payload.bins;
      state.data.warehouses = action.payload.warehouses;
    },
    setupsFailure: (state, action: PayloadAction<ApiError>) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const { setupsRequest, setupsSuccess, setupsFailure } = setupsSlice.actions;
export default setupsSlice.reducer;
