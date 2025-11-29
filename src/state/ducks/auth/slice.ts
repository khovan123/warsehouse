import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { LoginCredentials, LoginResponse } from '@/@types/auth';
import type { ApiError } from '@/apis/type';

import { INIT_AUTH } from './type';

const authSlice = createSlice({
  name: 'auth',
  initialState: INIT_AUTH,
  reducers: {
    loginRequest: (state, _action: PayloadAction<LoginCredentials>) => {
      state.loading = true;
      state.error = null;
    },
    loginSuccess: (state, action: PayloadAction<LoginResponse>) => {
      state.loading = false;
      state.data = action.payload;
      state.logined = true;
    },
    loginFailure: (state, action: PayloadAction<ApiError>) => {
      state.loading = false;
      state.error = action.payload;
    },
    logout: (state) => {
      state.data = null;
      state.error = null;
      state.loading = false;
      state.logined = false;
    },
  },
});
export const { loginRequest, loginSuccess, loginFailure, logout } = authSlice.actions;
export default authSlice.reducer;
