import { merge } from 'lodash';

import type { Movement, MovementLine } from '@/state/ducks/movement/type';

export type FlatedMovement = Omit<Movement, 'lines'> & MovementLine;

export const flatMovementData = (movements: Movement[] | []): FlatedMovement[] => {
  return movements.flatMap((movement) => {
    const { lines, ...rest } = movement;
    return (lines ?? []).map((line) => {
      return merge({ ...rest }, line);
    });
  });
};
