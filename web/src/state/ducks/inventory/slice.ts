import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_INVENTORY_STATE, type Inventory } from './type';

export const inventorySlice = createSlice({
  name: 'inventory',
  initialState: INIT_INVENTORY_STATE,
  reducers: {
    fetchInventoryRequest: (state) => {
      state.loading = true;
    },
    fetchInventorySuccess: (state, action: PayloadAction<Inventory[]>) => {
      state.data.inventories = action.payload;
      state.loading = false;
    },
    fetchInventoryFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { fetchInventoryRequest, fetchInventorySuccess, fetchInventoryFailure } =
  inventorySlice.actions;
export default inventorySlice.reducer;
