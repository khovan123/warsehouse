import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_STOCK_STATE, type Stock } from './type';

export const stockSlice = createSlice({
  name: 'stock',
  initialState: INIT_STOCK_STATE,
  reducers: {
    stockRequest: (state) => {
      state.loading = true;
    },
    stockSuccess: (state, action: PayloadAction<Stock[]>) => {
      state.data.stocks = action.payload;
      state.loading = false;
    },
    stockFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { stockRequest, stockSuccess, stockFailure } = stockSlice.actions;
export default stockSlice.reducer;
