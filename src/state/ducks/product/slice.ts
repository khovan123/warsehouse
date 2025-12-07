import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_PRODUCT_STATE, type Product } from './type';

export const productSlice = createSlice({
  name: 'product',
  initialState: INIT_PRODUCT_STATE,
  reducers: {
    productRequest: (state) => {
      state.loading = true;
    },
    productSuccess: (state, action: PayloadAction<Product[]>) => {
      state.data.products = action.payload;
      state.loading = false;
    },
    productFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { productRequest, productSuccess, productFailure } = productSlice.actions;
export default productSlice.reducer;
