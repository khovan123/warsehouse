export type ToastType = 'success' | 'error' | 'info' | 'warning';

export type ToastPayload = {
  id?: string | number;
  type: ToastType;
  message: string;
};

export type ToastState = {
  current?: ToastPayload | null;
};

export const INIT_TOAST: ToastState = {
  current: null,
};
