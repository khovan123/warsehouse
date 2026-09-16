import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { InventoryType } from '@/utils/constants';

import { INIT_MATERIAL_TRANSACTION_STATE, type MaterialTransactionState } from './type';

export const materialTransactionSlice = createSlice({
  name: 'materialTransaction',
  initialState: INIT_MATERIAL_TRANSACTION_STATE,
  reducers: {
    fetchMaterialTransactionRequest: (
      state,
      _action: PayloadAction<{ inventoryType?: InventoryType; warehouseId?: string }>
    ) => {
      state.loading = true;
    },
    fetchMaterialTransactionSuccess: (
      state,
      action: PayloadAction<Pick<MaterialTransactionState, 'data'>>
    ) => {
      state.loading = false;
      state.data.materialTransactions = action.payload.data.materialTransactions;
    },
    fetchMaterialTransactionFailure: (
      state,
      action: PayloadAction<Pick<MaterialTransactionState, 'error'>>
    ) => {
      state.loading = false;
      state.error = action.payload.error;
    },
  },
});

export const {
  fetchMaterialTransactionRequest,
  fetchMaterialTransactionSuccess,
  fetchMaterialTransactionFailure,
} = materialTransactionSlice.actions;

export default materialTransactionSlice.reducer;
