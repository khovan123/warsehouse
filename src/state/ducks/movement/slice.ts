import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import { INIT_MOVEMENT_STATE, type MovementState } from './type';

export const movementSlice = createSlice({
  name: 'movement',
  initialState: INIT_MOVEMENT_STATE,
  reducers: {
    fetchMovementRequest: (state) => {
      state.loading = true;
    },
    fetchMovementSuccess: (state, action: PayloadAction<Pick<MovementState, 'data'>>) => {
      state.loading = false;
      state.data.movements = action.payload.data.movements;
    },
    fetchMovementFailure: (state, action: PayloadAction<Pick<MovementState, 'error'>>) => {
      state.loading = false;
      state.error = action.payload.error;
    },
  },
});

export const { fetchMovementRequest, fetchMovementSuccess, fetchMovementFailure } =
  movementSlice.actions;

export default movementSlice.reducer;
