import type { MaterialTransaction } from '@/state/ducks/material-transaction/type';

export type FetchMaterialTransactionResponse = {
  materialTransactions: MaterialTransaction[];
};
