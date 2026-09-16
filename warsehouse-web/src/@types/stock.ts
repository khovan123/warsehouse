import type { Stock } from '@/state/ducks/stock/type';

export type FetchStockResponse = {
  stocks: Stock[];
};
