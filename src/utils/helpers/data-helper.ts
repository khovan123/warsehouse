import { merge } from 'lodash';

import type { GoodTransaction, GoodTransactionLine } from '@/state/ducks/good-transaction/type';
import type { Movement, MovementLine } from '@/state/ducks/movement/type';

export type FlatedMovement = Omit<Movement, 'lines'> & MovementLine;
export type FlattedGoodTransaction = Omit<GoodTransaction, 'lines'> & GoodTransactionLine;

export const flatMovementData = (movements: Movement[] | []): FlatedMovement[] => {
  return movements.flatMap((movement) => {
    const { lines, ...rest } = movement;
    return (lines ?? []).map((line) => {
      return merge({ ...rest }, line);
    });
  });
};

export const flatGoodTransactionData = (transactions: GoodTransaction[] | []) => {
  return transactions.flatMap((transaction) => {
    const { lines, ...rest } = transaction;
    return (lines ?? []).map((line) => {
      return merge({ ...rest }, line);
    });
  });
};
