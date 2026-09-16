import type { Movement, MovementReport, MovementSummary } from '@/state/ducks/movement/type';

export type FetchMovementResponse = {
  movements: Movement[] | [];
};

export type FetchMovementReportResponse = {
  movementReports: MovementReport[];
};

export type FetchMovementSummaryResponse = {
  movementSummaries: MovementSummary[];
};
