import type { GoodTransaction } from '@/state/ducks/good-transaction/type';

export type GoodTransactionResponse = {
  goodTransactions: GoodTransaction[] | [];
};
