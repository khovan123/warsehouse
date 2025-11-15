import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import { INIT_TOAST, type ToastPayload } from './type';

const toastSlice = createSlice({
  name: 'toast',
  initialState: INIT_TOAST,
  reducers: {
    showToast: (state, action: PayloadAction<ToastPayload>) => {
      state.current = {
        id: action.payload.id ?? `${Date.now()}-${Math.random()}`,
        type: action.payload.type,
        message: action.payload.message,
      };
    },
    clearToast: (state) => {
      state.current = null;
    },
  },
});

export const { showToast, clearToast } = toastSlice.actions;
export default toastSlice.reducer;
// 15112025 MinhPNQ ADD END
