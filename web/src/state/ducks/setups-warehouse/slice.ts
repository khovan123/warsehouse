import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_SETUPS_STATE, type Setups } from './type';

export const setupsSlice = createSlice({
  name: 'setups',
  initialState: INIT_SETUPS_STATE,
  reducers: {
    fetchSetupsWarehouseRequest: (state) => {
      state.loading = true;
    },
    fetchSetupsWarehouseSuccess: (state, action: PayloadAction<Setups>) => {
      state.loading = false;
      state.data.bins = action.payload.bins;
      state.data.warehouses = action.payload.warehouses;
    },
    fetchSetupsWarehouseFailure: (state, action: PayloadAction<ApiError>) => {
      state.loading = false;
      state.error = action.payload;
    },
  },
});

export const {
  fetchSetupsWarehouseRequest,
  fetchSetupsWarehouseSuccess,
  fetchSetupsWarehouseFailure,
} = setupsSlice.actions;
export default setupsSlice.reducer;
