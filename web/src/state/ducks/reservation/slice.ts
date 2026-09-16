import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import { INIT_RESERVATION_STATE, type ReservationState } from './type';

export const reservationSlice = createSlice({
  name: 'reservation',
  initialState: INIT_RESERVATION_STATE,
  reducers: {
    fetchReservationRequest: (state) => {
      state.loading = true;
    },
    fetchReservationSuccess: (state, action: PayloadAction<Pick<ReservationState, 'data'>>) => {
      state.loading = false;
      state.data.reservations = action.payload.data.reservations;
    },
    fetchReservationFailure: (state, action: PayloadAction<Pick<ReservationState, 'error'>>) => {
      state.loading = false;
      state.error = action.payload.error;
    },
  },
});

export const { fetchReservationRequest, fetchReservationSuccess, fetchReservationFailure } =
  reservationSlice.actions;

export default reservationSlice.reducer;
