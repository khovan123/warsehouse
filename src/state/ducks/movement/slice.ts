import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

import type { Period } from '@/utils/constants';

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
    fetchMovementReportRequest: (state) => {
      state.reportLoading = true;
    },
    fetchMovementReportSuccess: (state, action: PayloadAction<Pick<MovementState, 'data'>>) => {
      state.reportLoading = false;
      state.data.movementReports = action.payload.data.movementReports;
    },
    fetchMovementReportFailure: (state, action: PayloadAction<Pick<MovementState, 'error'>>) => {
      state.reportLoading = false;
      state.error = action.payload.error;
    },
    fetchMovementSummaryRequest: (state, _action: PayloadAction<{ period: Period }>) => {
      state.summaryLoading = true;
    },
    fetchMovementSummarySuccess: (state, action: PayloadAction<Pick<MovementState, 'data'>>) => {
      state.summaryLoading = false;
      state.data.movementSummaries = action.payload.data.movementSummaries;
    },
    fetchMovementSummaryFailure: (state, action: PayloadAction<Pick<MovementState, 'error'>>) => {
      state.summaryLoading = false;
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
  fetchMovementSummaryRequest,
  fetchMovementSummarySuccess,
  fetchMovementSummaryFailure,
} = movementSlice.actions;

export default movementSlice.reducer;
