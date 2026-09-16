import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { InventoryType, Period } from '@/utils/constants';

import { INIT_MOVEMENT_STATE, type MovementReportSuccessPayload, type MovementState } from './type';

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
    fetchMovementReportRequest: (
      state,
      _action: PayloadAction<{ inventoryType?: InventoryType; period?: Period }>
    ) => {
      state.loading = true;
    },
    fetchMovementReportSuccess: (state, action: PayloadAction<MovementReportSuccessPayload>) => {
      const { movementReports, movementSummaries } = action.payload;
      state.loading = false;
      if (movementReports) {
        state.data.movementReports = movementReports;
      }
      if (movementSummaries) {
        state.data.movementSummaries = movementSummaries;
      }
    },
    fetchMovementReportFailure: (state, action: PayloadAction<Pick<MovementState, 'error'>>) => {
      state.loading = false;
      state.error = action.payload.error;
    },
  },
});

export const {
  fetchMovementRequest,
  fetchMovementSuccess,
  fetchMovementFailure,
  fetchMovementReportRequest,
  fetchMovementReportSuccess,
  fetchMovementReportFailure,
} = movementSlice.actions;

export default movementSlice.reducer;
