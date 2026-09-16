import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_INVENTORY_STATE, type Inventory } from './type';

export const inventorySlice = createSlice({
  name: 'inventory',
  initialState: INIT_INVENTORY_STATE,
  reducers: {
    inventoriesRequest: (state) => {
      state.loading = true;
    },
    inventoriesSuccess: (state, action: PayloadAction<Inventory[]>) => {
      state.data.inventories = action.payload;
      state.loading = false;
    },
    inventoriesFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { inventoriesRequest, inventoriesSuccess, inventoriesFailure } =
  inventorySlice.actions;
export default inventorySlice.reducer;
