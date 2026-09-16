import { createSlice, type PayloadAction } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';

import { INIT_TOAST, type ToastPayload } from './type';

const toastSlice = createSlice({
  name: 'toast',
  initialState: INIT_TOAST,
  reducers: {
    showToast: (state, action: PayloadAction<ToastPayload>) => {
      const { type, message, id } = action.payload;
      const fn =
        type === 'success'
          ? toast.success
          : type === 'error'
            ? toast.error
            : type === 'info'
              ? toast.info
              : toast.warn;
      toast.dismiss(state.current?.id);
      const newId = fn(message, { toastId: id });
      state.current = {
        id: newId,
        type: type,
        message: message,
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
