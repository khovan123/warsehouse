export type ToastType = 'success' | 'error' | 'info' | 'warning';

export type ToastPayload = {
  id?: string;
  type: ToastType;
  message: string;
};

export type ToastState = {
  current?: ToastPayload | null;
};

export const INIT_TOAST: ToastState = {
  current: null,
};
