import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import { INIT_GOODTRANSACTION_STATE, type GoodTransactionState } from './type';

export const goodTransactionSlice = createSlice({
  name: 'goodTransaction',
  initialState: INIT_GOODTRANSACTION_STATE,
  reducers: {
    fetchGoodTransactionRequest: (state) => {
      state.loading = true;
    },
    fetchGoodTransactionSuccess: (
      state,
      action: PayloadAction<Pick<GoodTransactionState, 'data'>>
    ) => {
      state.loading = true;
      state.data.goodTransactions = action.payload.data.goodTransactions;
    },
    fetchGoodTransactionFailure: (
      state,
      action: PayloadAction<Pick<GoodTransactionState, 'error'>>
    ) => {
      state.loading = false;
      state.error = action.payload.error;
    },
  },
});

export const {
  fetchGoodTransactionRequest,
  fetchGoodTransactionSuccess,
  fetchGoodTransactionFailure,
} = goodTransactionSlice.actions;

export default goodTransactionSlice.reducer;
