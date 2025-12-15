import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { ApiError } from '@/apis/type';

import { INIT_PRODUCT_STATE, type Product } from './type';

export const productSlice = createSlice({
  name: 'product',
  initialState: INIT_PRODUCT_STATE,
  reducers: {
    fetchProductRequest: (state) => {
      state.loading = true;
    },
    fetchProductSuccess: (state, action: PayloadAction<Product[]>) => {
      state.data.products = action.payload;
      state.loading = false;
    },
    fetchProductFailure: (state, action: PayloadAction<ApiError>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { fetchProductRequest, fetchProductSuccess, fetchProductFailure } =
  productSlice.actions;
export default productSlice.reducer;
