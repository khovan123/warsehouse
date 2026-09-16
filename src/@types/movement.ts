import type { Movement } from '@/state/ducks/movement/type';

export type FetchMovementResponse = {
  movements: Movement[] | [];
};
