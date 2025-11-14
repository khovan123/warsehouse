import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { LoginCredentials, LoginResponse } from '../../../@types/auth';

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
      state.data = { name: action.payload.id.toString() };
      state.logined = true;
    },
    loginFailure: (state, action) => {
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
