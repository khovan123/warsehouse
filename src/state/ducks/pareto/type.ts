import type { ApiError } from '@/apis/type';

import type { ParetoClassTag } from './constants';

export type Pareto = {
  id: string;
  productId: string;
  categoryId: string;
  product: string;
  category: string;
  tag: ParetoClassTag;
  annualConsumption: number;
  value: number;
};

export type ParetoClassification = {
  tag: ParetoClassTag;
  tagPercentage: number;
  valuePercentage: number;
};

export type ParetoState = {
  data: {
    paretos: Pareto[] | null;
    classifications: ParetoClassification[] | null;
  };
  loading: boolean;
  error: ApiError | null;
};

export const INIT_PARETO_STATE: ParetoState = {
  data: {
    paretos: null,
    classifications: null,
  },
  loading: false,
  error: null,
};
